using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupSpawnPoint spawnPointParent;

    private void Start()
    {
        
    }

    public virtual void PickupObject()
    {
        transform.SetParent(Player.Instance.transform);
        transform.position = Player.Instance.transform.position;
    }
}
