using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupSpawnPoint spawnPointParent;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Options_AudioVolume");
    }

    public virtual void PickupObject()
    {
        transform.SetParent(Player.Instance.transform);
        audioSource.Play();
    }
}
