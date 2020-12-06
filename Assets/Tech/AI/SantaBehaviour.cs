using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Goes back and forth between the movement points, periodically lunges at player.
/// </summary>
public class SantaBehaviour : AIBehaviour {
  public List<Sprite> standingAnimation = new List<Sprite>();

  public List<Vector3> movementPoints = new List<Vector3>();
  public float lungeSpeed = 20;

  private int currentPointIndex = 0;
  private float lastLungeTime = 0.0f;
  private float timeBetweenLunges;
  private Vector3 lastPlayerPosition;
  private bool isLunging;
  private Vector3 moveDirection = Vector3.zero;

  private float baseLungeTime;
  private float baseLungeSpeed;
  private bool canLunge = false;
  private float lastMoveSpeed;

  private float lastDifficultyIncreaseStep = 1;
  private float healthDifficultyIncreaseStep = 0.25f;

  // Start is called before the first frame update
  void Start() {
    lastLungeTime = Time.time;


  }

  private void CheckHealthDifficultyIncrease(float current, float max) {
    if (current / max <= lastDifficultyIncreaseStep - healthDifficultyIncreaseStep) {
      GameManager.Instance.IncreaseDifficulty();
      lastDifficultyIncreaseStep -= healthDifficultyIncreaseStep;
    }
  }

  public override void InitializeAI(AIController controller) {
    base.InitializeAI(controller);
    controller.health.OnHealthChanged += CheckHealthDifficultyIncrease;
    baseLungeSpeed = lungeSpeed;
    baseLungeTime = 4;
    timeBetweenLunges = 4;
    lastLungeTime = Time.time;
    controller.movement.movementSpeed = 0;
    controller.movement.overrideAnimations = true;
    controller.movement.currentAnimation = standingAnimation;
  }

  // Update is called once per frame
  void Update() {
    if (GameManager.Instance.gameFrozen)
      return;

    if (controller == null)
      return;

    if (Time.time - lastLungeTime > timeBetweenLunges && canLunge) {
      LungeAtPlayer();
      lastLungeTime = Time.time;
    }

    if (!isLunging) {
      UpdateMovement();
    } else {
      UpdateLunging();
    }

    controller.movement.SetMovementVector(moveDirection);

    foreach (ProjectileWeapon weapon in controller.projectileWeapons) {
      if (weapon.enabled)
        weapon.Shoot((Player.Instance.transform.position - transform.position).normalized, AgentType.Boss);
    }
  }

  private void UpdateMovement() {
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

  public override void UpdateDifficulty() {
    base.UpdateDifficulty();
    controller.movement.overrideAnimations = false;
    lastLungeTime = Time.time;
    canLunge = true;
    lungeSpeed = baseLungeSpeed * GameManager.Instance.difficulty;
    timeBetweenLunges = baseLungeTime / GameManager.Instance.difficulty;
    if (isLunging) {
      lastMoveSpeed = controller.movement.movementSpeed;
      controller.movement.movementSpeed = lungeSpeed;
    }
  }

  private void LungeAtPlayer() {
    if (isLunging)
      return;
    lastMoveSpeed = controller.movement.movementSpeed;
    lastPlayerPosition = Player.Instance.transform.position;
    controller.movement.overrideAnimations = true;
    controller.movement.movementSpeed = lungeSpeed;
    isLunging = true;
  }

  private void UpdateLunging() {
    moveDirection = (lastPlayerPosition - transform.position).normalized;
    float distance = Vector3.Distance(transform.position, lastPlayerPosition);
    if (distance <= 0.1f) {
      isLunging = false;
      controller.movement.movementSpeed = lastMoveSpeed;
    }
  }
}
