using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource starsSound;
    public AudioSource recordSound;
    public AudioSource scoreSound;

    private Dictionary<string, AudioSource> _audioSources;

    private void Awake()
    {
        _audioSources = new Dictionary<string, AudioSource>();
        _audioSources.Add("stars", starsSound);
        _audioSources.Add("record", recordSound);
        _audioSources.Add("score", scoreSound);
    }

    public void PlaySound(string soundName)
    {
        _audioSources[soundName].Play();
    }
}
