using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour {
  public AgentType agentType;
  public float maxHealth = 100;
  private float currentHealth;
  public event Action<float, float> OnHealthChanged;

  private void Awake() {
    maxHealth = currentHealth;
    Healthbar.LinkToHealthbar(this);
  }

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }

  public void TakeDamage(float amount) {
    currentHealth -= amount;
    OnHealthChanged?.Invoke(currentHealth, maxHealth);

    if (currentHealth <= 0)
      Kill();
  }

  public void Kill() {
    /// kill funct. depends on agent type.
  }
}
