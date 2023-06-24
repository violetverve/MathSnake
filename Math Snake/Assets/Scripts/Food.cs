using UnityEngine;
using TMPro;

public class Food : MonoBehaviour
{

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

}
