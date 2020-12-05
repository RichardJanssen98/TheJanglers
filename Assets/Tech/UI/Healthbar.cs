using System.Collections;
using System.Collections.Generic;
using ElRaccoone.Tweens;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
  public AgentType agentType;
  public Health agentHealth;
  public Image healthbarContainer;
  public Image healthBarFill;
  public Color healthbarColorMax;
  public Color healthbarColorMin;
  private bool initialized = false;

  public static List<Healthbar> healthbars = new List<Healthbar>();

  public static void LinkToHealthbar(Health health) {
    foreach (Healthbar hb in healthbars) {
      if (hb.agentType == health.agentType) {
        hb.agentHealth = health;
        hb.Initialize();
        return;
      }
    }
  }

  private void Awake() {
    healthbars.Add(this);
  }

  private void Start() {
    if (agentHealth != null)
      Initialize();
  }

  public void Initialize() {
    if (initialized)
      return;

    agentHealth.OnHealthChanged += OnHealthChanged;

    if (initialized) {
      healthBarFill.TweenImageFillAmount(1, 0.5f).SetFrom(0);
    }
  }

  public void OnHealthChanged(float health, float maxHealth) {
    Debug.Log(health);
    healthBarFill.TweenImageFillAmount(health / maxHealth, 0.5f);
    Color barColor = Color.Lerp(healthbarColorMax, healthbarColorMin, health / maxHealth);
    healthBarFill.TweenGraphicColor(barColor, 0.5f);
  }
}
