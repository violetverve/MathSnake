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
    public TextMeshProUGUI soundButtonText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;


    private bool _isFirstPlay = true;
    private GameManager _gameManager;
    private bool _isSoundOn = true;


    private void Awake()
    {
        _isFirstPlay = PlayerPrefs.GetInt("FirstPlay", 1) == 1;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        SetSound(_isSoundOn);
    }

    public void Start()
    {
        InitializeToggleKeyMap();
        SetToggles();

        if (_isFirstPlay)
        {
            gameOverMenu.SetActive(true);
            statisticsPanel.SetActive(false);
            PlayerPrefs.SetInt("FirstPlay", 0);
        }
        else
        {
            gameOverMenu.SetActive(false);
        }
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
        if (_gameManager.GetInitialSetupDone() && _isSoundOn)
        {
            clickSound.Play();
        }
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("FirstPlay", 1);
        Application.Quit();
    }

    public void ChangeSoundSetting()
    {
        _isSoundOn = !_isSoundOn;
        SetSound(_isSoundOn);
        PlayerPrefs.SetInt("Sound", _isSoundOn ? 1 : 0);
    }

    private void SetSound(bool isSoundOn)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        SetSoundButtonSprite(isSoundOn);

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = !isSoundOn;
        }
    }

    private void SetSoundButtonSprite(bool isSoundOn)
    {
        soundButtonText.text = isSoundOn ? "<sprite name=\"SoundOn\">" : "<sprite name=\"SoundOff\">";
    }
}