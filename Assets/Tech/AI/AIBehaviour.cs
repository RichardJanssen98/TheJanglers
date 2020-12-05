using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour {
  protected AIController controller;

  // Base stats
  protected float baseHealth;
  protected Dictionary<ProjectileWeapon, float> baseWeaponCooldowns = new Dictionary<ProjectileWeapon, float>();
  protected float baseMoveSpeed;

  protected static List<AIBehaviour> aiBehaviours = new List<AIBehaviour>();

  private void Awake() {
    aiBehaviours.Add(this);
  }

  public virtual void InitializeAI(AIController controller) {
    this.controller = controller;
    baseHealth = controller.health.maxHealth;
    foreach (ProjectileWeapon weapon in controller.projectileWeapons) {
      baseWeaponCooldowns.Add(weapon, weapon.shootTimerCooldown);
    }
    baseMoveSpeed = controller.movement.movementSpeed;

    baseHealth *= GameManager.Instance.baseDifficulty;
    controller.health.maxHealth = baseHealth;
    controller.health.currentHealth = baseHealth;

    controller.health.maxHealth *= GameManager.Instance.baseDifficulty;
    controller.health.currentHealth *= GameManager.Instance.baseDifficulty;

    foreach (ProjectileWeapon weapon in controller.projectileWeapons) {
      weapon.shootTimerCooldown /= GameManager.Instance.baseDifficulty;
    }
  }

  public static void UpdateAllAIDifficulties() {
    foreach (AIBehaviour ai in aiBehaviours)
      ai.UpdateDifficulty();
  }

  public virtual void UpdateDifficulty() {
    foreach (KeyValuePair<ProjectileWeapon, float> weapon in baseWeaponCooldowns) {
      weapon.Key.shootTimerCooldown = weapon.Value / GameManager.Instance.difficulty;
    }
    controller.movement.movementSpeed = baseMoveSpeed * GameManager.Instance.difficulty;
  }

  private void OnDestroy() {
    aiBehaviours.Remove(this);
  }
}
