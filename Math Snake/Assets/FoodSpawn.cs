using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodSpawn : MonoBehaviour
{
    public GameObject foodPrefab;
    public BoxCollider2D gridArea;

    private List<Transform> _foods = new List<Transform>();

    public int numberOfFood = 1;

    void Start()
    {
        SpawnFoods();
    }

    public void SpawnFood()
    {       
        GameObject newFood = Instantiate(foodPrefab, RandomizePosition(), Quaternion.identity);

        _foods.Add(newFood.transform);
    }


    public void SetFoodValues(int answer)
    {
        for (int i = 0; i < _foods.Count; i++)
        {
            TextMeshPro textComponent = _foods[i].Find("Number").GetComponent<TextMeshPro>();
            if (i == 0)
            {
                textComponent.text = answer.ToString();
            }
            else
            {
                int value = Random.Range(answer - 5, answer + 5);
                textComponent.text = value.ToString();
            } 
        }
    }

    public void SpawnFoods()
    {
        for (int i = 0; i < numberOfFood; i++)
        {
            SpawnFood();
        }
    }

    private Vector3 RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    public void ChangeFoodPosition()
    {
        foreach (Transform food in _foods)
        {
            food.position = RandomizePosition();
        }
    }
}
