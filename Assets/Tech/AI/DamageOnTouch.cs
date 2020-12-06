using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour {
  public float damageOnTouch = 5;

  private void OnTriggerEnter2D(Collider2D collision) {
    Health health = collision.GetComponent<Health>();
    if (health != null && health.agentType == AgentType.Player) {
      health.TakeDamage(damageOnTouch);
    }
  }
}
