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
            player.projectileWeapon.Shoot();
        }
    }

    public bool ShootButtonPressed()
    {
        if (Input.GetAxis("Fire") > 0)
        {
            return true;
        }
        return false;
    }

    public Vector2 MovementPressed()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector2(horizontal, vertical);
    }
}
