using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShootButtonPressed())
        {
            if (player.projectileWeapon != null)
            {
                var mouse = Input.mousePosition;
                var screenPoint = Camera.main.ScreenToWorldPoint(mouse);
                Vector2 direction = ((Vector2)screenPoint - (Vector2)transform.position).normalized;

                player.projectileWeapon.Shoot(direction);
            }
        }
        if (GrabButtonPressed())
        {
            player.grab.GrabClosestPickup();
        }

        player.movement.SetMovementVector(GetMovement());
    }

    public bool GrabButtonPressed()
    {
        return Input.GetButtonDown("Use");
    }

    public bool ShootButtonPressed()
    {
        return Input.GetMouseButton(0);
    }

    public Vector2 GetMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector2(horizontal, vertical);
    }
}
