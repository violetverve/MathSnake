using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpritesComplexityGameOver : MonoBehaviour
{
    public TextMeshProUGUI complexitySpritesText;
    public SnakeColorDropdown snakeColorDropdown;
    public ComplexityDropdown speedDropdown;
    public FruitDropdown fruitDropdown;

    private void SetComplexitySpritesText()
    {
        complexitySpritesText.text = $"{speedDropdown.GetSelectedValue()}" +
            $" {snakeColorDropdown.GetSelectedValue()}" +
            $" {fruitDropdown.GetSelectedValue()}";
    }

    public void SetTexts()
    {
        SetComplexitySpritesText();
    }
}
