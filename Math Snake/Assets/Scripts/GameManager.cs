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

    private int _score;
    private bool _showTongue;

    private bool _initialSetupDone = false;

    private void Awake()
    {
        StartCoroutine(InitializeGameCoroutine());
    }


    private IEnumerator InitializeGameCoroutine()
    {
        Instance = this;
        _snakeScript = snakeObject.GetComponent<Snake>();
        _uiManager = UIManagerObject.GetComponent<UIManager>();
        _particleManager = particleObject.GetComponent<ParticleManager>();

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
        string best = $"BestScore-{PlayerPrefs.GetInt("Speed")}-{PlayerPrefs.GetInt("Fruit")}";
        if (_score > PlayerPrefs.GetInt(best, 0))
        {
            PlayerPrefs.SetInt(best, _score);
        }
    }

    private void ResetFood()
    {
        GameObject.Find("FoodSpawn").GetComponent<FoodSpawn>().ChangeFoodPosition();
        mathUnit.ExecuteRandomOperation();
    }

    public void HandleFoodCollision(Food food)
    {
        crunchSound.Play();
        if (mathUnit.CheckAnswer(food.GetValue()))
        {
            _snakeScript.Grow();
        }
        else
        {
            StartCoroutine(DeathCoroutine(() => _particleManager.PlayMathParticle(
                GetVector3From2(_snakeScript.GetGridPosition()))));
        }
        ResetFood();
    }

    public void HandleSnakeCollision()
    {
        hitSound.Play();
        StartCoroutine(DeathCoroutine(() => _particleManager.PlayHitParticle(
            GetVector3From2(_snakeScript.GetGridPosition()))));
    }

    public void IncreaseScore(int amount)
    {
        _score += amount;
        if (_initialSetupDone) _uiManager.SetScoreText(_score);
    }

    public void LoadBestScore()
    {
        string best = $"BestScore-{PlayerPrefs.GetInt("Speed")}-{PlayerPrefs.GetInt("Fruit")}";

        int bestScore = PlayerPrefs.GetInt(best, 0);
        bool hasBestScore = bestScore > 0;

        bestScorePanel.SetActive(hasBestScore);
        _uiManager.SetBestScoreText(bestScore);
    }

    public void HandleTongueAnimation()
    {
        _showTongue = false;

        Vector3 headPosition = GetVector3From2(_snakeScript.GetGridPosition());
        List<Vector3> foodPositions = GameObject.Find("FoodSpawn").GetComponent<FoodSpawn>().GetFoodPositions();

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