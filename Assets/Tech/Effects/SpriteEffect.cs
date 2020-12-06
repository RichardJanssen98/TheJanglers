using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class SpriteEffect : MonoBehaviour {

  public Vector3 startPosition = Vector3.one;
  public Vector3 endPosition = Vector3.one;
  public Vector3 startRotation = Vector3.one;
  public Vector3 endRotation = Vector3.one;
  public Vector3 startScale = Vector3.one;
  public Vector3 endScale = Vector3.one;

  public bool startOnAwake;
  public bool bounce;
  public bool loop;
  public float interval;
  public float speed;
  public bool useTTL;
  public float ttl;

  void Awake() {
    if (!startOnAwake)
      return;

    StartEffect();
  }

  // Update is called once per frame
  void Update() {
    if (!useTTL)
      return;

    ttl -= Time.deltaTime;
    if (ttl <= 0) {
      gameObject.TweenCancelAll();
      Destroy(gameObject);
    }
  }

  public void StartEffect() {
    DoPosition();
    DoRotation();
    DoScale();
  }

  void DoPosition() {
    if (startPosition == endPosition)
      return;

    gameObject.TweenPosition(endPosition, 1 / speed).SetFrom(startPosition).SetOnComplete(() => {
      if (bounce)
        gameObject.TweenPosition(startPosition, 1 / speed).SetFrom(endPosition).SetOnComplete(() => {
          if (loop)
            if (interval > 0)
              gameObject.TweenDelayedInvoke(interval, DoPosition);
            else
              DoPosition();
        });
    });
  }

  void DoRotation() {
    if (startRotation == endRotation)
      return;

    gameObject.TweenRotation(endRotation, 1 / speed).SetFrom(startRotation).SetOnComplete(() => {
      if (bounce)
        gameObject.TweenRotation(startRotation, 1 / speed).SetFrom(endRotation).SetOnComplete(() => {
          if (loop)
            if (interval > 0)
              gameObject.TweenDelayedInvoke(interval, DoRotation);
            else
              DoRotation();
        });
    });
  }

  void DoScale() {
    if (startScale == endScale)
      return;

    gameObject.TweenLocalScale(endScale, 1 / speed).SetFrom(startScale).SetOnComplete(() => {
      if (bounce)
        gameObject.TweenLocalScale(startScale, 1 / speed).SetFrom(endScale).SetOnComplete(() => {
          if (loop)
            if (interval > 0)
              gameObject.TweenDelayedInvoke(interval, DoScale);
            else
              DoScale();
        });
    });
  }
}
