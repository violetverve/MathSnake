using UnityEngine;
using TMPro;

public class ComplexityDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private const string _dropdownPrefsKey = "Speed";

    private void Start()
    {
        LoadDropdownValue();

        dropdown.onValueChanged.AddListener(delegate { SaveDropdownValue(); });
    }

    private void SaveDropdownValue()
    {
        PlayerPrefs.SetInt(_dropdownPrefsKey, dropdown.value);
    }

    private void LoadDropdownValue()
    {
        dropdown.value = PlayerPrefs.GetInt(_dropdownPrefsKey, 0);
    }

    public void SetSpeed()
    {
        LoadDropdownValue();

        float[] speedValues = { 0.2f, 0.1f, 0.075f };

        if (dropdown.value >= 0 && dropdown.value < speedValues.Length)
        {
            Time.fixedDeltaTime = speedValues[dropdown.value];
        }
    }

    public string GetSelectedValue()
    {
        return dropdown.options[dropdown.value].text;
    }
}
