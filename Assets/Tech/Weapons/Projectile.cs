using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
  public AgentType agentType;
  public float damage;
  public float movementSpeed;

  public Vector2 direction;

  public float lifeTime = 10;

  // Update is called once per frame
  void Update() {
    if (GameManager.Instance.gameFrozen)
      return;

    lifeTime -= Time.deltaTime;
    if (lifeTime <= 0) {
      Destroy(gameObject);
      return;
    }

    transform.position += (Vector3)direction * (movementSpeed * Time.deltaTime);
  }

}
