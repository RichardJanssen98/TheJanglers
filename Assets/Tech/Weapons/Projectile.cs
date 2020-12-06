using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class Projectile : MonoBehaviour
{
    public AgentType agentType;
    public float damage;
    public float movementSpeed;
    public bool trackPlayer;
    private float trackTimerStart = 0f;
    public float trackTimerCooldown = 2f;

    public Vector2 direction;

    public float lifeTime = 10;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameFrozen)
            return;

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            gameObject.TweenLocalScale(Vector3.zero, 0.2f).SetOnComplete(() =>
            {
                Destroy(gameObject);
            });
            return;

        }
        if (Time.time > trackTimerStart + trackTimerCooldown && trackPlayer)
        {
            trackTimerStart = Time.time;
            direction = (Player.Instance.transform.position - transform.position).normalized;
        }
        transform.position += (Vector3)direction * (movementSpeed * Time.deltaTime);
        transform.up = direction;
    }


}
