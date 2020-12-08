using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUpdater : MonoBehaviour
{
    public AudioSource audioSource;
    // Update is called once per frame
    private void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Options_AudioVolume");
    }

    void Update()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Options_AudioVolume");
    }
}
