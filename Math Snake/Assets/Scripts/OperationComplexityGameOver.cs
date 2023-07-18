using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OperationComplexityGameOver : MonoBehaviour
{
    public TextMeshProUGUI complexityAddSubText;
    public TextMeshProUGUI complexityMulDivText;

    public Toggle _additionToggle;
    public Toggle _subtractionToggle;
    public Toggle _multiplicationToggle;
    public Toggle _divisionToggle;

    public Color activeColor;

    private string _activeColorHex;

    private void Awake()
    {
        _activeColorHex = ColorUtility.ToHtmlStringRGB(activeColor);
    }

    private void SetAddSubComplexityText()
    {
        string plus = _additionToggle.isOn ? $"<color=#{_activeColorHex}>+</color>" : "+";
        string minus = _subtractionToggle.isOn ? $"<color=#{_activeColorHex}>-</color>" : "-";
        complexityAddSubText.text = $"{plus} {minus} : {PlayerPrefs.GetInt("SliderAddSub", 10)}";
    }

    private void SetMulDivComplexityText()
    {
        int complexityValue = PlayerPrefs.GetInt("SliderMulDiv", 10);
        string multiplication = $"<sprite name=\"MultiplicationSign\" {(_multiplicationToggle.isOn ? $"color=#{_activeColorHex}" : "")}>";
        string division = $"<sprite name=\"DivisionSign\" {(_divisionToggle.isOn ? $"color=#{_activeColorHex}" : "")}>";
        string complexityText = $"{multiplication} {division} : {complexityValue}";
        complexityMulDivText.text = complexityText;
    }

    public void SetTexts()
    {
        SetAddSubComplexityText();
        SetMulDivComplexityText();
    }
}
