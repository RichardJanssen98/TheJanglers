using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
  public Movement movement;
  public Health health;
  public ProjectileWeapon projectileWeapon;
  public AIBehaviour behaviour;


  // Start is called before the first frame update
  void Start() {
    behaviour.InitializeAI(this);
  }

  // Update is called once per frame
  void Update() {

  }
}
