using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeColorDropdown : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;
    public GameObject snake;
    public GameObject snakeSegmentPrefab;

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
                SetColor(Color.green);
                break;
            case 1:
                SetColor(Color.yellow);
                break;
            case 2:
                SetColor(Color.blue);
                break;
        }
    }
    
    private void SetColor(Color color)
    {
        snake.GetComponent<Renderer>().material.color = color;
        snakeSegmentPrefab.GetComponent<SpriteRenderer>().color = color;
    }



}
