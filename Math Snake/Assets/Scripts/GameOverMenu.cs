using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public OperationComplexityGameOver operationComplexityGameOver;
    public ScoresContainerGameOver scoresContainerGameOver;
    public SpritesComplexityGameOver spritesComplexityGameOver;

    public void SetTexts()
    {
        operationComplexityGameOver.SetTexts();
        scoresContainerGameOver.SetTexts();
        spritesComplexityGameOver.SetTexts();
    }
}
