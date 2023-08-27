using UnityEngine;
using TMPro;

public class RevealButton : MonoBehaviour
{
    public TextMeshProUGUI revealButtonText;
    private bool _isRevealed;

    private void Start()
    {
        _isRevealed = true;
        ChangeTextVisibility();
    }

    public void ChangeTextVisibility()
    {
        _isRevealed = !_isRevealed;
        revealButtonText.enabled = _isRevealed;
    }
}
