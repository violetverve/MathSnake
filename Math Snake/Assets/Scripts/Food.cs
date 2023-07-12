using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{
    public int timeToGrow;
    private Snake _snakeScript;

    private int _timeCounter;

    private float _growthAmount;

    private bool _isGrowing;

    private void Awake()
    {
        _timeCounter = 0;
        _growthAmount = 0.15f;
        _isGrowing = true;
        _snakeScript = GameObject.Find("Snake").GetComponent<Snake>();
    }

    public int GetValue()
    {
        Transform numberTransform = transform.Find("Number");

        if (numberTransform != null)
        {
            TextMeshPro textComponent = numberTransform.GetComponent<TextMeshPro>();

            if (textComponent != null)
            {
                string text = textComponent.text;
                int value;
                if (int.TryParse(text, out value))
                {
                    return value;
                }
            }
        }

        return 0;
    }

    private void FixedUpdate()
    {
        if (!_snakeScript.GetAlive()) return;

        transform.localScale += _isGrowing ? new Vector3(_growthAmount, _growthAmount, _growthAmount)
                                        : new Vector3(-_growthAmount, -_growthAmount, -_growthAmount);

        if (++_timeCounter >= timeToGrow)
        {
            _isGrowing = !_isGrowing;
            _timeCounter = 0;
        }
    }

}
