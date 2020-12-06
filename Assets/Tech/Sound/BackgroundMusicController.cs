using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : Singleton<BackgroundMusicController>
{
    public AudioSource audioSource;
    public AudioClip introSoundtrack;
    public AudioClip fightSoundtrack;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Options_AudioVolume");
        audioSource.loop = true;
    }

    public void PlayIntroSoundtrack()
    {
        audioSource.clip = introSoundtrack;
        audioSource.Play();
    }

    public void PlayFightSoundtrack()
    {
        audioSource.Stop();
        audioSource.clip = fightSoundtrack;

        audioSource.Play();
    }
}
