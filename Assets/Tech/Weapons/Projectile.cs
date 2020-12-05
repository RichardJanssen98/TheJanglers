using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float movementSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (movementSpeed * Time.deltaTime);
    }
}
