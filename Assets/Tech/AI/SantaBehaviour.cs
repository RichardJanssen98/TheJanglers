using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Goes back and forth between the movement points, periodically lunges at player.
/// </summary>
public class SantaBehaviour : AIBehaviour {
  public List<Vector3> movementPoints = new List<Vector3>();
  public float maxTimeBetweenLunges = 5;
  public float minTimeBetweenLunges = 1;
  public float lungeSpeed = 20;
  public float moveSpeed = 10;

  private AIController controller;
  private int currentPointIndex = 0;
  private float lastLungeTime = 0.0f;
  private float timeBetweenLunges;
  private Vector3 lastPlayerPosition;
  private bool isLunging;
  private Vector3 moveDirection = Vector3.zero;

  // Start is called before the first frame update
  void Start() {
    lastLungeTime = Time.time;
  }

  // Update is called once per frame
  void Update() {
    if (controller == null)
      return;

    timeBetweenLunges = Mathf.Lerp(minTimeBetweenLunges, maxTimeBetweenLunges, controller.health.currentHealth / controller.health.maxHealth);
    if (Time.time - lastLungeTime > timeBetweenLunges) {
      LungeAtPlayer();
      lastLungeTime = Time.time;
    }

    if (!isLunging) {
      UpdateMovement();
    } else {
      UpdateLunging();
    }

    controller.movement.SetMovementVector(moveDirection);
  }

  public override void InitializeAI(AIController controller) {
    this.controller = controller;
    lastLungeTime = Time.time;
  }

  private void UpdateMovement() {
    controller.movement.movementSpeed = moveSpeed;
    Vector3 point = movementPoints[currentPointIndex];
    float distance = Vector3.Distance(transform.position, point);
    if (distance < 0.1f) {
      currentPointIndex++;
      if (currentPointIndex >= movementPoints.Count)
        currentPointIndex = 0;
    }
    point = movementPoints[currentPointIndex];
    moveDirection = (point - transform.position).normalized;
  }

  private void LungeAtPlayer() {
    lastPlayerPosition = Player.Instance.transform.position;
    controller.movement.overrideAnimations = true;
    controller.movement.movementSpeed = lungeSpeed;
    isLunging = true;
  }

  private void UpdateLunging() {
    moveDirection = (lastPlayerPosition - transform.position).normalized;
    float distance = Vector3.Distance(transform.position, lastPlayerPosition);
    if (distance <= 0.1f)
      isLunging = false;
  }
}
