using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupSpawnPoint spawnPointParent;
    public virtual void PickupObject()
    {
        transform.SetParent(Player.Instance.transform);
    }
}
