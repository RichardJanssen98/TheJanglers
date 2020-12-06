using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : Singleton<BackgroundMusicController>
{

    public AudioSource introSoundtrack;
    public AudioSource fightSoundtrack;

    // Start is called before the first frame update
    void Start()
    {
        introSoundtrack.volume = PlayerPrefs.GetFloat("Options_AudioVolume");
        fightSoundtrack.volume = PlayerPrefs.GetFloat("Options_AudioVolume");
    }

    public void PlayIntroSoundtrack()
    {
        introSoundtrack.loop = true;
        introSoundtrack.Play();
    }

    public void PlayFightSoundtrack()
    {
        introSoundtrack.Stop();

        fightSoundtrack.loop = true;
        fightSoundtrack.Play();
    }
}
