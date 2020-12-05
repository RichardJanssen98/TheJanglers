using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    private float spawnTimerStart = 0f;
    public float spawnTimerCooldown = 3f;
    public List<Present> presentsList;

    public List<Transform> spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        GetAllSpawnpoints();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnInPresent();
    }

    private void SpawnInPresent()
    {
        if (Time.time > spawnTimerStart + spawnTimerCooldown)
        {
            spawnTimerStart = Time.time;

            int presentNumber = Random.Range(0, presentsList.Count); //-1 because it starts counting at 0
            Present presentToSpawn = presentsList[presentNumber];

            int spawnLocationNumber = Random.Range(0, spawnLocations.Count);
            Transform spawnLocation = spawnLocations[spawnLocationNumber];

            Instantiate(presentToSpawn.gameObject, spawnLocation.position, Quaternion.identity);
        }
    }

    private void GetAllSpawnpoints()
    {
        //Get all spawn locations, these should all be children from this object
        Transform[] pickupSpawnPoints = GetComponentsInChildren<Transform>();

        for (int i = 0; i < pickupSpawnPoints.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }
            spawnLocations.Add(pickupSpawnPoints[i]);
        }
    }
}
