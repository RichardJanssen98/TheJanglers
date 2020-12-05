using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;


    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        GameObject.Instantiate(projectile, this.transform.position, this.transform.rotation);
    }
}
