using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class StatisticsPanel : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    public float fontSizeAnimationDuration = 0.5f;
    public float fontSizeIncreaseAmount = 6f;

    private Coroutine scoreTextAnimationCoroutine;

    public void SetScoreText(int score)
    {
        if (scoreTextAnimationCoroutine != null)
            StopCoroutine(scoreTextAnimationCoroutine);

        scoreTextAnimationCoroutine = StartCoroutine(AnimateFontSize(scoreText, score, true));
    }

    public void SetBestScoreText(int bestScore)
    {
        bestScoreText.text = $"<sprite name=\"Cup\"> {bestScore}";
    }

    private IEnumerator AnimateFontSize(TextMeshProUGUI textComponent, int targetScore, bool increaseSize)
    {
        float initialFontSize = textComponent.fontSize;
        float targetFontSize = increaseSize ? initialFontSize + fontSizeIncreaseAmount : initialFontSize;

        float elapsedTime = 0f;
        while (elapsedTime < fontSizeAnimationDuration)
        {
            float t = elapsedTime / fontSizeAnimationDuration;
            textComponent.fontSize = Mathf.Lerp(initialFontSize, targetFontSize, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.fontSize = targetFontSize;

        if (increaseSize)
            scoreText.text = $"<sprite name=\"Apple\"> {targetScore}";

        while (elapsedTime < fontSizeAnimationDuration * 2)
        {
            float t = (elapsedTime - fontSizeAnimationDuration) / fontSizeAnimationDuration;
            textComponent.fontSize = Mathf.Lerp(targetFontSize, initialFontSize, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.fontSize = initialFontSize;
    }
}