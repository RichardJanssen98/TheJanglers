using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerBehaviour : AIBehaviour {
  public float updatePositionInterval = 5;

  private float lastTrackTime;
  private Vector3 lastKnownPlayerPosition;
  private Vector3 direction;

  private float baseInterval = 5;

  // Start is called before the first frame update
  void Start() {

  }

  public override void InitializeAI(AIController controller) {
    base.InitializeAI(controller);
  }

  // Update is called once per frame
  void Update() {
    if (Time.time - lastTrackTime >= updatePositionInterval) {
      lastKnownPlayerPosition = Player.Instance.transform.position;
      lastTrackTime = Time.time;
    }

    direction = (lastKnownPlayerPosition - transform.position).normalized;
    controller.movement.SetMovementVector(direction);
    if (Vector3.Distance(transform.position, lastKnownPlayerPosition) < 0.1f) {
      controller.movement.SetMovementVector(Vector3.zero);
    }

    foreach (ProjectileWeapon weapon in controller.projectileWeapons) {
      if (weapon.enabled)
        weapon.Shoot((Player.Instance.transform.position - transform.position).normalize, false, AgentType.Enemy);
    }
  }

  public override void UpdateDifficulty() {
    base.UpdateDifficulty();
    updatePositionInterval = baseInterval / GameManager.Instance.difficulty;
  }
}
