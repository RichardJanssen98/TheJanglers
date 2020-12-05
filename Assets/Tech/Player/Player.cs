using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;

    public ProjectileWeapon projectileWeapon;
    public Movement movement;
    public Grab grab;

    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Player>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("PlayerSingleton");
                    _instance = container.AddComponent<Player>();
                }
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
