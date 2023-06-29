using System;
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

    private Snake _snakeScript;
    private int _score;
    private void Awake()
    {
        Instance = this;
        _snakeScript = snakeObject.GetComponent<Snake>();
        complexityDropdown.SetSpeed();
        snakeColorDropdown.SetSnakeColor();
        _score = -_snakeScript.GetInitialSize() + 1;
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

}