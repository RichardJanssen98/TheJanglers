using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;

    private float shootTimerStart = 0f;
    public float shootTimerCooldown = 1f;

    public float angle;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (Time.time > shootTimerStart + shootTimerCooldown)
        {
            shootTimerStart = Time.time;
            

            GameObject.Instantiate(projectile, this.transform.position, Quaternion.identity);
        }
    }
}
