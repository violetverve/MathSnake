using UnityEngine;

public class ComplexityDropdown : MonoBehaviour
{

    public TMPro.TMP_Dropdown dropdown;

    private void Start()
    {
        LoadDropdownValue(dropdown.name);
        SetSpeed();
        dropdown.onValueChanged.AddListener(delegate {SaveDropdownValue(dropdown.name); });
    }

    public void SaveDropdownValue(string key)
    {
        PlayerPrefs.SetInt(key, dropdown.value);
    }

    public void LoadDropdownValue(string key)
    {
        dropdown.value = PlayerPrefs.GetInt(key, 1);
    }
 
    public void SetSpeed()
    {
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
