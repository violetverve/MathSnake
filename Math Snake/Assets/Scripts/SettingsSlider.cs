using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;

    private void Start()
    {
        LoadSliderValue(slider.name);
        UpdateSliderText();
        slider.onValueChanged.AddListener(delegate { UpdateSliderText(); SaveSliderValue(slider.name); });
    }

    private void UpdateSliderText()
    {
        sliderText.text = slider.value.ToString();
    }

    public void SaveSliderValue(string key)
    {
        PlayerPrefs.SetInt(key, (int)slider.value);
    }

    public void LoadSliderValue(string key)
    {
        slider.value = PlayerPrefs.GetInt(key, 1);
    }
}
