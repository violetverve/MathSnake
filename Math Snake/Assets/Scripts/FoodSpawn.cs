using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodSpawn : MonoBehaviour
{
    public GameObject foodPrefab;
    public BoxCollider2D gridArea;
    public FruitDropdown fruitDropdown;
    public int numberOfFood;

    private List<Transform> _foods;
    private List<Vector3> _positions;
    private Snake _snake;

    private void Awake()
    {
        _foods = new List<Transform>();
        _positions = new List<Vector3>();
        _snake = GameObject.Find("Snake").GetComponent<Snake>();
    }

    void Start()
    {
        fruitDropdown.SetFoodNumber();
        SpawnFoods();
    }

    public void SpawnFood(Vector3 position)
    {
        GameObject newFood = Instantiate(foodPrefab, position, Quaternion.identity);

        _foods.Add(newFood.transform);
    }


    public void SetFoodValues(List<int> values)
    {
        for (int i = 0; i < _foods.Count; i++)
        {
            TextMeshPro textComponent = _foods[i].Find("Number").GetComponent<TextMeshPro>();
            textComponent.text = values[i].ToString();
        }
    }

    public void SpawnFoods()
    {
        List<Vector3> positions = RandomizePositions();
        for (int i = 0; i < numberOfFood; i++)
        {
            SpawnFood(positions[i]);
        }
    }

    private List<Vector3> RandomizePositions()
    {
        List<Vector3> positions = new List<Vector3>();
        List<Vector3> snakePositions = _snake.GetSnakePositions();

        for (int i = 0; i < numberOfFood; i++)
        {
            Vector3 position = RandomizePosition();
            while (positions.Contains(position) || snakePositions.Contains(position))
            {
                position = RandomizePosition();
            }
            positions.Add(position);
        }

        _positions = positions;

        return positions;
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
        List<Vector3> positions = RandomizePositions();

        for (int i = 0; i < _foods.Count; i++)
        {
            _foods[i].position = positions[i];
        }
    }

    public List<Vector3> GetFoodPositions()
    {
        return _positions;
    }
}
