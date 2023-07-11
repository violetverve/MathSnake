using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject statisticsPanel;
    public GameObject settingsPanel;

    public Toggle _additionToggle;
    public Toggle _subtractionToggle;
    public Toggle _multiplicationToggle;
    public Toggle _divisionToggle;
    private static Dictionary<Toggle, string> toggleKeyMap;
    public AudioSource clickSound;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    public void Start()
    {
        gameOverMenu.SetActive(false);
        InitializeToggleKeyMap();
        SetToggles();
    }

    private void InitializeToggleKeyMap()
    {
        toggleKeyMap = new Dictionary<Toggle, string>
        {
            { _additionToggle, nameof(OperationType.Addition) },
            { _subtractionToggle, nameof(OperationType.Subtraction) },
            { _multiplicationToggle, nameof(OperationType.Multiplication) },
            { _divisionToggle, nameof(OperationType.Division) }
        };
    }

    private void OnEnable()
    {
        Snake.OnPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        Snake.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        settingsPanel.SetActive(false);
        statisticsPanel.SetActive(false);
        SetScoreText();
        gameOverMenu.SetActive(true);
    }

    public void RestartGame()
    {
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnableSettingsPanel()
    {
        gameOverMenu.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void SaveSettings()
    {
        foreach (var kvp in toggleKeyMap)
        {
            Toggle toggle = kvp.Key;
            string playerPrefsKey = kvp.Value;
            PlayerPrefs.SetInt(playerPrefsKey, toggle.isOn ? 1 : 0);
        }
    }

    public void SetToggles()
    {
        foreach (var kvp in toggleKeyMap)
        {
            Toggle toggle = kvp.Key;
            string playerPrefsKey = kvp.Value;
            toggle.isOn = PlayerPrefs.GetInt(playerPrefsKey) == 1;
        }
    }

    public void SetScoreText()
    {
        string best = $"BestScore-{PlayerPrefs.GetInt("Speed")}-{PlayerPrefs.GetInt("Fruit")}";
        if (PlayerPrefs.HasKey(best))
        {
            bestScoreText.text = $"<sprite name=\"Cup\"> {PlayerPrefs.GetInt(best)}";
        }
        else
        {
            bestScoreText.text = "";
        }
        scoreText.text = $"<sprite name=\"Apple\"> {PlayerPrefs.GetInt("Score", 0)}";
    }

    public void PlayClickSound()
    {
        clickSound.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}