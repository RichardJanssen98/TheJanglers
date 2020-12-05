using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnPoint : MonoBehaviour
{
    public bool occupied = false;

    PickupSpawner pickupSpawner;

    private void Start()
    {
        pickupSpawner = gameObject.GetComponentInParent<PickupSpawner>();
    }

    public bool SpawnPresent(GameObject presentToSpawn)
    {
        if (occupied)
        {
            return false;
        }
        else
        {
            GameObject presentThatSpawned = Instantiate(presentToSpawn.gameObject, this.transform.position, Quaternion.identity);
            presentThatSpawned.GetComponent<Pickup>().spawnPointParent = this;
            occupied = true;
            return true;
        }
        
    }
}
