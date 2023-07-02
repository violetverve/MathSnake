using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject snakeObject;
    public MathUnit mathUnit;
    public TextMeshProUGUI scoreText;
    public ComplexityDropdown complexityDropdown;
    public SnakeColorDropdown snakeColorDropdown;
    public Animator animator;

    private Snake _snakeScript;
    private int _score;
    private bool _showTongue;

    private void Awake()
    {
        Instance = this;
        _snakeScript = snakeObject.GetComponent<Snake>();
        complexityDropdown.SetSpeed();
        snakeColorDropdown.SetSnakeColor();
        _score = -_snakeScript.GetInitialSize() + 1;
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

    private void SaveScore()
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
        if (mathUnit.CheckAnswer(food.GetValue()))
        {
            _snakeScript.Grow();
        }
        else
        {
            SaveScore();
            _snakeScript.Die();
        }
        ResetFood();
    }

    public void HandleObstacleCollision()
    {
        SaveScore();
        _snakeScript.Die();
    }

    public void IncreaseScore(int amount)
    {
        _score += amount;
        scoreText.text = $"Score: {_score.ToString()}";
    }

    public void HandleTongueAnimation()
    {
        _showTongue = false;

        Vector3 headPosition = new Vector3(_snakeScript.GetGridPosition().x, _snakeScript.GetGridPosition().y, 0);
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

}