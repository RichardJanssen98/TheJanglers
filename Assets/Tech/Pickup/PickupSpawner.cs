using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    private float spawnTimerStart = 0f;
    public float spawnTimerCooldown = 3f;
    public List<Present> presentsList;

    public List<PickupSpawnPoint> spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        GetAllSpawnpoints();
    }

    // Update is called once per frame
    void Update()
    {
        TimerForSpawning();
    }

    private void TimerForSpawning()
    {
        if (Time.time > spawnTimerStart + spawnTimerCooldown)
        {
            SpawnInPresent();
        }
    }

    private void SpawnInPresent()
    {
        spawnLocations.Shuffle();
        spawnTimerStart = Time.time;

        if (spawnLocations.Count != 0)
        {
            int presentNumber = Random.Range(0, presentsList.Count); //-1 because it starts counting at 0
            Present presentToSpawn = presentsList[presentNumber];

            foreach (PickupSpawnPoint p in spawnLocations)
            {
                if (!p.occupied)
                {
                    p.SpawnPresent(presentToSpawn.gameObject);
                    break;
                }        
            }    
        }
    }

    private void GetAllSpawnpoints()
    {
        //Get all spawn locations, these should all be children from this object
        PickupSpawnPoint[] pickupSpawnPoints = GetComponentsInChildren<PickupSpawnPoint>();

        spawnLocations.AddRange(pickupSpawnPoints);
    }
}
