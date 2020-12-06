using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Goes back and forth between the movement points, periodically lunges at player.
/// </summary>
public class SantaBehaviour : AIBehaviour {
  public List<Sprite> standingAnimation = new List<Sprite>();
  public List<Sprite> chargeAnimation = new List<Sprite>();
  public List<Sprite> attackAnimation = new List<Sprite>();
  public Sprite lungeAnimation;
  public float shootTime = 1;

  public List<Vector3> movementPoints = new List<Vector3>();
  public float lungeSpeed = 20;

  private int currentPointIndex = 0;
  private float lastLungeTime = 0.0f;
  private float timeBetweenLunges;
  private Vector3 lastPlayerPosition;
  private bool isLunging;
  private bool isShooting;
  private Vector3 moveDirection = Vector3.zero;

  private float baseLungeTime;
  private float baseLungeSpeed;
  private bool canLunge = false;
  private float lastMoveSpeed;
  private float lastShotTime;

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
    lastShotTime = Time.time;
    controller.movement.movementSpeed = 0;
    controller.movement.overrideAnimations = true;
    controller.movement.overridePlayAnimation = false;
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
      if (Time.time - lastShotTime > shootTime && !isShooting) {
        ShootAtPlayer();
      }
    } else {
      UpdateLunging();
    }

    controller.movement.SetMovementVector(moveDirection);
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
    controller.movement.overridePlayAnimation = false;
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
    controller.movement.movementSpeed = 0;

    StopAllCoroutines();
    StartCoroutine(CycleThroughAnimation(chargeAnimation, 0.5f, () => {
      lastPlayerPosition = Player.Instance.transform.position;
      controller.movement.overrideAnimations = true;
      controller.movement.overridePlayAnimation = true;
      controller.movement.movementSpeed = lungeSpeed;
      isLunging = true;
      controller.movement.charSpriteRenderer.sprite = lungeAnimation;
    }));
  }

  private void UpdateLunging() {
    moveDirection = (lastPlayerPosition - transform.position).normalized;
    float distance = Vector3.Distance(transform.position, lastPlayerPosition);
    if (distance <= 0.1f) {
      isLunging = false;
      controller.movement.movementSpeed = lastMoveSpeed;
      controller.movement.overrideAnimations = false;
      controller.movement.overridePlayAnimation = false;
    }
  }

  private void ShootAtPlayer() {
    isShooting = true;
    controller.movement.overrideAnimations = true;
    controller.movement.overridePlayAnimation = true;

    StopAllCoroutines();
    StartCoroutine(CycleThroughAnimation(attackAnimation, 1f, () => {
      lastShotTime = Time.time;
      isShooting = false;
      foreach (ProjectileWeapon weapon in controller.projectileWeapons)
        weapon.Shoot(transform.position - lastPlayerPosition, AgentType.Boss);
      IdleStand();
    }));
  }

  private void IdleStand() {
    if (canLunge) {
      controller.movement.overrideAnimations = false;
      controller.movement.overridePlayAnimation = false;
      return;
    }

    StopAllCoroutines();
    StartCoroutine(CycleThroughAnimation(standingAnimation, 0.5f, () => {
      IdleStand();
    }));
  }

  private IEnumerator CycleThroughAnimation(List<Sprite> animation, float duration, System.Action callback) {
    int index = 0;
    while (index < animation.Count) {
      controller.movement.charSpriteRenderer.sprite = animation[index];
      float animCount = animation.Count;
      index++;
      yield return new WaitForSeconds(duration / animCount);
    }
    callback?.Invoke();
  }
}
