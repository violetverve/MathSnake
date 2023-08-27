using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject snakeObject;
    public GameObject particleObject;
    public GameObject UIManagerObject;

    public MathUnit mathUnit;

    public GameObject bestScorePanel;
    public ComplexityDropdown complexityDropdown;
    public SnakeColorDropdown snakeColorDropdown;
    public Animator animator;
    public AudioSource crunchSound;
    public AudioSource hitSound;

    private Snake _snakeScript;
    private ParticleManager _particleManager;
    private UIManager _uiManager;
    private FoodSpawn _foodSpawn;
    private AudioManager _audioManager;

    private int _score;
    private bool _showTongue;

    private bool _initialSetupDone;
    private int _initialBestScore;
    private bool _recordBroken;
    private string _bestScoreKey;

    private void Awake()
    {
        _bestScoreKey = $"BestScore-{PlayerPrefs.GetInt("Speed")}-{PlayerPrefs.GetInt("Fruit")}";
        _initialSetupDone = false;
        _recordBroken = false;
        _initialBestScore = PlayerPrefs.GetInt(_bestScoreKey, 0);

        StartCoroutine(InitializeGameCoroutine());
    }


    private IEnumerator InitializeGameCoroutine()
    {
        Instance = this;
        _snakeScript = snakeObject.GetComponent<Snake>();
        _uiManager = UIManagerObject.GetComponent<UIManager>();
        _particleManager = particleObject.GetComponent<ParticleManager>();
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _foodSpawn = GameObject.Find("FoodSpawn").GetComponent<FoodSpawn>();

        SetDropdowns();
        _score = -_snakeScript.GetInitialSize() + 1;

        yield return null;
        LoadBestScore();
        _initialSetupDone = true;
    }

    private void FixedUpdate()
    {
        if (_snakeScript.GetAlive())
        {
            HandleTongueAnimation();
        }
        else
        {
            animator.SetBool("ShowTongue", false);
        }

    }

    private void SaveScores()
    {
        PlayerPrefs.SetInt("Score", _score);

        if (_recordBroken) PlayerPrefs.SetInt(_bestScoreKey, _score);
    }

    private void ResetFood()
    {
        _foodSpawn.ChangeFoodPosition();
        mathUnit.ExecuteRandomOperation();
    }

    public void HandleFoodCollision(Food food)
    {
        crunchSound.Play();
        if (mathUnit.CheckAnswer(food.GetValue()))
        {
            _snakeScript.Grow();
            UpdateBestScore();
        }
        else
        {
            StartCoroutine(DeathCoroutine(() => _particleManager.PlayParticle("math")));
            _audioManager.PlaySound("stars");
        }
        ResetFood();
    }

    public void HandleSnakeCollision()
    {
        hitSound.Play();
        StartCoroutine(DeathCoroutine(() => _particleManager.PlayParticle("hit")));
        _audioManager.PlaySound("stars");
    }

    public void IncreaseScore(int amount)
    {
        _score += amount;
        if (_initialSetupDone) _uiManager.SetScoreText(_score);
    }

    public void LoadBestScore()
    {
        bool hasBestScore = _initialBestScore > 0;
        bestScorePanel.SetActive(hasBestScore);
        _uiManager.SetInitialBestScoreText(_initialBestScore);
    }

    private void UpdateBestScore()
    {
        if (!_recordBroken && _score > _initialBestScore)
        {
            _recordBroken = true;
            _particleManager.PlayParticle("record");
            _audioManager.PlaySound("record");
        }
        else
        {
            _particleManager.PlayParticle("score");
            _audioManager.PlaySound("score");
        }

        if (_recordBroken) _uiManager.SetBestScoreText(_score);
    }

    public void HandleTongueAnimation()
    {
        _showTongue = false;

        Vector3 headPosition = GetVector3From2(_snakeScript.GetGridPosition());
        List<Vector3> foodPositions = _foodSpawn.GetFoodPositions();

        for (int i = 0; i < foodPositions.Count; i++)
        {
            if (GetDistance(headPosition, foodPositions[i]) < 4f)
            {
                _showTongue = true;
                break;
            }
        }

        animator.SetBool("ShowTongue", _showTongue);
    }

    private float GetDistance(Vector3 position1, Vector3 position2)
    {
        return Mathf.Sqrt(Mathf.Pow(position1.x - position2.x, 2) + Mathf.Pow(position1.y - position2.y, 2));
    }

    public bool GetInitialSetupDone()
    {
        return _initialSetupDone;
    }

    private IEnumerator DeathCoroutine(Action particleAction)
    {
        _snakeScript.DisableMovement();
        particleAction.Invoke();
        yield return new WaitForSeconds(2.5f);
        SaveScores();
        _snakeScript.Die();
    }

    public Vector3 GetVector3From2(Vector2 gridPosition)
    {
        return new Vector3(gridPosition.x, gridPosition.y, 0);
    }

    public void SetDropdowns()
    {
        complexityDropdown.SetSpeed();
        snakeColorDropdown.SetSnakeColor();
    }

}