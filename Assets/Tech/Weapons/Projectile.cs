using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float movementSpeed;

    public Vector2 direction;

    public float lifeTime = 10;

    private void Start()
    {
        GameObject.Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * (movementSpeed * Time.deltaTime);
    }

}
