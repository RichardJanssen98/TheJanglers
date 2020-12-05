using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float movementSpeed;

    Vector2 direction;

    public float lifeTime = 10;

    private void Start()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.ScreenToWorldPoint(mouse);
        direction = (screenPoint - transform.position).normalized;

        GameObject.Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * (movementSpeed * Time.deltaTime);
    }

}
