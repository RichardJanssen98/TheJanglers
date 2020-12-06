using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
  Player player;

  private void Start() {
    player = Player.Instance;
    player.movement.overrideAnimations = true;
  }

  // Update is called once per frame
  void Update() {
    if (GameManager.Instance.gameFrozen)
      return;

    Vector3 mouse = Input.mousePosition;
    Vector3 screenPoint = Camera.main.ScreenToWorldPoint(mouse);
    Vector2 lookDir = screenPoint - player.transform.position;

    if (Mathf.Abs(lookDir.x) > Mathf.Abs(lookDir.y)) {
      player.movement.currentAnimation = player.movement.spritesHorizontal;
      if (lookDir.x < 0) {
        player.movement.mirror = false;
      } else {
        player.movement.mirror = true;
      }
    } else {
      player.movement.mirror = false;
      if (lookDir.y > 0) {
        player.movement.currentAnimation = player.movement.spritesUp;
      } else {
        player.movement.currentAnimation = player.movement.spritesDown;
      }
    }

    if (ShootButtonPressed()) {
      if (player.projectileWeapon != null) {
        Vector2 direction = ((Vector2)screenPoint - (Vector2)transform.position).normalized;

        player.projectileWeapon.Shoot(direction, AgentType.Player);
      }
    }
    if (GrabButtonPressed()) {
      player.grab.GrabClosestPickup();
    }

    player.movement.SetMovementVector(GetMovement());
  }

  public bool GrabButtonPressed() {
    return Input.GetButtonDown("Use");
  }

  public bool ShootButtonPressed() {
    return Input.GetMouseButton(0);
  }

  public Vector2 GetMovement() {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    return new Vector2(horizontal, vertical);
  }
}
