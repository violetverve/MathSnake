using UnityEngine;
using TMPro;

public class ScoresContainerGameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    public void SetBestScore()
    {
        string best = $"BestScore-{PlayerPrefs.GetInt("Speed")}-{PlayerPrefs.GetInt("Fruit")}";

        bestScoreText.text = PlayerPrefs.HasKey(best) ? $"<sprite name=\"Cup\"> {PlayerPrefs.GetInt(best)}" : "";
    }

    public void SetScore()
    {
        scoreText.text = $"<sprite name=\"Apple\"> {PlayerPrefs.GetInt("Score", 0)}";
    }

    public void SetTexts()
    {
        SetBestScore();
        SetScore();
    }
}
