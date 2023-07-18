using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeColorDropdown : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;
    public GameObject snake;
    public GameObject snakeSegmentPrefab;

    public Sprite snakeHeadGreen;
    public Sprite snakeHeadYellow;
    public Sprite snakeHeadBlue;

    public Sprite snakeBodyGreen;
    public Sprite snakeBodyYellow;
    public Sprite snakeBodyBlue;

    private const string DropdownPrefsKey = "SnakeColor";

    public void SaveSnakeColor()
    {
        PlayerPrefs.SetInt(DropdownPrefsKey, dropdown.value);
    }

    public void LoadSnakeColor()
    {
        dropdown.value = PlayerPrefs.GetInt(DropdownPrefsKey, 0);
    }

    public void SetSnakeColor()
    {
        LoadSnakeColor();

        switch (dropdown.value)
        {
            case 0:
                SetSprite(snakeHeadGreen, snakeBodyGreen);
                break;
            case 1:
                SetSprite(snakeHeadYellow, snakeBodyYellow);
                break;
            case 2:
                SetSprite(snakeHeadBlue, snakeBodyBlue);
                break;
        }
    }

    private void SetSprite(Sprite headSprite, Sprite bodySprite)
    {
        snake.GetComponent<SpriteRenderer>().sprite = headSprite;
        snakeSegmentPrefab.GetComponent<SpriteRenderer>().sprite = bodySprite;
    }
    public string GetSelectedValue()
    {
        return dropdown.options[dropdown.value].text;
    }

}
