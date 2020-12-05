using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {

  public ProjectileWeapon projectileWeapon;
  public Movement movement;
  public Grab grab;

  // Start is called before the first frame update
  void Start() {
    DontDestroyOnLoad(this);
  }

  public void OnTriggerEnter2D(Collider2D collision) {

  }
}
