using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Pickup
{
    [SerializeField]
    private Projectile projectile;

    private float shootTimerStart = 0f;
    public float shootTimerCooldown = 1f;
    public float ammo = 10f;
    public float ammoConsumption = 1f;

    // Update is called once per frame
    void Update()
    {
        if (ammo <= 0)
        {
            Destroy(this.gameObject);
            Player.Instance.projectileWeapon = null;
        }
    }

    public override void PickupObject()
    {
        base.PickupObject();
        Player.Instance.projectileWeapon = this;
        AmmoBar.Instance.ClearAmmo();
        AmmoBar.Instance.FillUpAmmo((int)ammo);
    }

    public void Shoot(Vector2 direction, bool isPlayer, AgentType agentType)
    {
        if (Time.time > shootTimerStart + shootTimerCooldown)
        {
            shootTimerStart = Time.time;
            
            Projectile projectileSpawned = Instantiate(projectile, this.transform.position, Quaternion.identity);
            projectileSpawned.direction = direction;
            projectileSpawned.agentType = agentType;

            if (isPlayer)
            {
                ReduceAmmo();
            }
        }
    }

    public void ReduceAmmo()
    {
        ammo -= ammoConsumption;
        AmmoBar.Instance.RemoveAmmo((int)ammoConsumption);
    }
}
