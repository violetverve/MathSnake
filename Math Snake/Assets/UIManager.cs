using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void Start()
    {
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
        gameOverMenu.SetActive(true);
    }

    public void RestartGame()
    {
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
}
