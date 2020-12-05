using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private InputManager inputManager;

    // Update is called once per frame
    void Update()
    {
        if (inputManager.ShootButtonPressed())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject.Instantiate(projectile, this.transform.position, this.transform.rotation);
    }
}
