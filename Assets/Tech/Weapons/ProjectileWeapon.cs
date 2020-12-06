using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Pickup {
  [SerializeField]
  private Projectile projectile;

    public AudioSource audioSource;
    public AudioClip shootingSoundClip;

    public List<Sprite> SpritesUp;
    public List<Sprite> SpritesDown;
    public List<Sprite> SpritesHorizontal;

  private float shootTimerStart = 0f;
  public float shootTimerCooldown = 1f;
  public float ammo = 10f;
  public float ammoConsumption = 1f;
  public float projectilesPerShot = 1f;
  public float projectileDamageOverride = 5f;


    private void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Options_AudioVolume");
    }

    // Update is called once per frame
    void Update() {
    if (ammo <= 0) {
      Destroy(this.gameObject);
      Player.Instance.projectileWeapon = null;
            Player.Instance.movement.holdingWeapon = false;
    }
  }

  public override void PickupObject() {
    base.PickupObject();
    Player.Instance.projectileWeapon = this;
        Player.Instance.movement.spritesWeaponUp = SpritesUp;
        Player.Instance.movement.spritesWeaponDown = SpritesDown;
        Player.Instance.movement.spritesWeaponHorizontal = SpritesHorizontal;
        Player.Instance.movement.holdingWeapon = true;
    AmmoBar.Instance.ClearAmmo();
    AmmoBar.Instance.FillUpAmmo((int)ammo);
  }

  public void Shoot(Vector2 direction, AgentType agentType) {
    if (Time.time > shootTimerStart + shootTimerCooldown) {
      shootTimerStart = Time.time;

      if (projectilesPerShot % 2 == 0) {
        projectilesPerShot++;
      }

      float directionDeviation = 0;
            audioSource.PlayOneShot(shootingSoundClip);

      for (int i = 0; i < projectilesPerShot; i++) {
        Projectile projectileSpawned = Instantiate(projectile, this.transform.position, Quaternion.identity);
        Vector2 directionToGo;

        projectileSpawned.agentType = agentType;

        if (i % 2 != 0) {
          directionDeviation = Mathf.Abs(directionDeviation) + (0.1f);
          directionToGo = new Vector2(direction.x + directionDeviation, direction.y - directionDeviation);
        } else {
          directionDeviation *= -1;
          directionToGo = new Vector2(direction.x + directionDeviation, direction.y + directionDeviation);
        }
        projectileSpawned.direction = directionToGo;
      }


      if (agentType == AgentType.Player) {
        ReduceAmmo();
      }
    }
  }

  public void ReduceAmmo() {
    ammo -= ammoConsumption;
    AmmoBar.Instance.RemoveAmmo((int)ammoConsumption);
  }
}
