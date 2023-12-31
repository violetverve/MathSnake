using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSwitch : MonoBehaviour
{
    public TextMeshProUGUI soundButtonText;
    private bool _isSoundOn;


    public void ChangeSoundSetting()
    {
        _isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1 ? false : true;
        SetSound(_isSoundOn);
        PlayerPrefs.SetInt("Sound", _isSoundOn ? 1 : 0);
    }

    private void SetSound(bool isSoundOn)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        SetSoundButtonSprite(isSoundOn);

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = !isSoundOn;
        }
    }

    private void SetSoundButtonSprite(bool isSoundOn)
    {
        soundButtonText.text = isSoundOn ? "<sprite name=\"SoundOn\">" : "<sprite name=\"SoundOff\">";
    }
}
