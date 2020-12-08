using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ElRaccoone.Tweens;

public class Health : MonoBehaviour {
  public AgentType agentType;
  public float maxHealth = 100;
  public float currentHealth;
  public event Action<float, float> OnHealthChanged;
  public AudioSource audioSource;
  public bool invulnerable;
  public GameObject deathEffect;

  public AudioClip deathSoundClip;

  private void Awake() {
    currentHealth = maxHealth;

  }

  // Start is called before the first frame update
  void Start() {
    audioSource.volume = PlayerPrefs.GetFloat("Options_AudioVolume");
    Healthbar.LinkToHealthbar(this);
  }

  // Update is called once per frame
  void Update() {
        if (Application.isEditor == false)
            return;
    if (Input.GetKeyDown(KeyCode.K)) {
            if (agentType == AgentType.Player)
            {
                TakeDamage(100);
            }
    }
    if (Input.GetKey(KeyCode.G))
        {
            if (agentType == AgentType.Boss)
            {
                TakeDamage(500);
            }
        }
  }

  public void TakeDamage(float amount) {
    if (invulnerable)
      return;
    currentHealth -= amount;
    OnHealthChanged?.Invoke(currentHealth, maxHealth);

    if (agentType == AgentType.Player) {
      invulnerable = true;
      FlickerSprite(0);
    }

    if (currentHealth <= 0)
      Kill();
  }

  public void Kill() {
    switch (agentType) {
      case AgentType.Player:
        GameManager.Instance.GameOver();
        break;
      case AgentType.Boss:
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        GameManager.Instance.CompleteGame();
        break;
      case AgentType.Enemy:
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(deathSoundClip);
        gameObject.TweenDelayedInvoke(deathSoundClip.length, () => Destroy(gameObject));
        break;
    }
  }

  private void FlickerSprite(float step) {
    if (step == 20) {
      invulnerable = false;
      Player.Instance.movement.charSpriteRenderer.TweenSpriteRendererAlpha(1, 0.1f);
      return;
    }
    float currentAlpha = Player.Instance.movement.charSpriteRenderer.color.a;
    float nextAlpha = currentAlpha == 1 ? 0 : 1;
    Player.Instance.movement.charSpriteRenderer.TweenSpriteRendererAlpha(nextAlpha, 0.1f).SetOnComplete(() => {
      step++;
      FlickerSprite(step);
    });
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    Projectile projectile = collision.GetComponent<Projectile>();

    if (projectile != null && projectile.agentType != this.agentType) {
            if (this.agentType == AgentType.Boss && projectile.agentType == AgentType.Enemy)
            {
                Destroy(projectile.gameObject);
            }
            else
            {
                TakeDamage(projectile.damage);
                Destroy(projectile.gameObject);
            }
    }
  }
}
