using UnityEngine;

public class ComplexityDropdown : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;

    private const string DropdownPrefsKey = "Speed";

    private void Start()
    {
        LoadDropdownValue();
        dropdown.onValueChanged.AddListener(delegate { SaveDropdownValue();});
    }

    private void SaveDropdownValue()
    {
        PlayerPrefs.SetInt(DropdownPrefsKey, dropdown.value);
    }

    private void LoadDropdownValue()
    {
        dropdown.value = PlayerPrefs.GetInt(DropdownPrefsKey, 0);
    }

    public void SetSpeed()
    {
        LoadDropdownValue();

        switch (dropdown.value)
        {
            case 0:
                Time.fixedDeltaTime = 0.2f;
                break;
            case 1:
                Time.fixedDeltaTime = 0.1f;
                break;
            case 2:
                Time.fixedDeltaTime = 0.05f;
                break;
        }
    }
}
