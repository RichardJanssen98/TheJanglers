using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Pickup
{
    [SerializeField]
    private Projectile projectile;

    private float shootTimerStart = 0f;
    public float shootTimerCooldown = 1f;

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void PickupObject()
    {
        base.PickupObject();
        Player.Instance.projectileWeapon = this;
    }

    public void Shoot(Vector2 direction)
    {
        if (Time.time > shootTimerStart + shootTimerCooldown)
        {
            shootTimerStart = Time.time;
            

            Projectile projectileSpawned = Instantiate(projectile, this.transform.position, Quaternion.identity);
            projectileSpawned.direction = direction;
        }
    }
}
