using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDropdown : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;

    public FoodSpawn foodSpawn;

    private const string DropdownPrefsKey = "Fruit";

    public void Start()
    {
        LoadFruit();
        SetFoodNumber();
        dropdown.onValueChanged.AddListener(delegate { SaveFruit();});
    }

    public void SaveFruit()
    {
        PlayerPrefs.SetInt(DropdownPrefsKey, dropdown.value);
    }

    public void LoadFruit()
    {
        dropdown.value = PlayerPrefs.GetInt(DropdownPrefsKey, 0);
    }

    public void SetFoodNumber()
    {
        LoadFruit();

        foodSpawn.numberOfFood = dropdown.value + (1*dropdown.value + 3);
    }
}
