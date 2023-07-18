using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatisticsPanel : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    public void SetScoreText(int score)
    {
        scoreText.text = $"<sprite name=\"Apple\"> {score}";
    }

    public void SetBestScoreText(int bestScore)
    {
        bestScoreText.text = $"<sprite name=\"Cup\"> {bestScore}";
    }
}
