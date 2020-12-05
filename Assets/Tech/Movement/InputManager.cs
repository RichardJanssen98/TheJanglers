﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
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
