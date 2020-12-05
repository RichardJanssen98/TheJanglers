using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    Vector2 movementDirection;
    [SerializeField]
    private float movementSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move()
    {
        movementDirection = inputManager.MovementPressed();
        transform.position = transform.position + ((Vector3)movementDirection * (movementSpeed * Time.deltaTime));
    }
}
