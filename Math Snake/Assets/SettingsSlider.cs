using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SettingsSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { UpdateSliderText(); });
    }

    private void UpdateSliderText()
    {
        sliderText.text = slider.value.ToString();
    }
}
