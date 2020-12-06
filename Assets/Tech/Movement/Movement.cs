using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
  public SpriteRenderer charSpriteRenderer;
  public float walkAnimSpeed = 1;
  public List<Sprite> spritesUp = new List<Sprite>();
  public List<Sprite> spritesDown = new List<Sprite>();
  public List<Sprite> spritesHorizontal = new List<Sprite>();
  public bool overrideAnimations = false;
  public bool overridePlayAnimation = false;
  public bool mirror = false;

  public List<Sprite> currentAnimation;
  private int currentSpriteIndex;
  private float walkAnimTime;

  public Vector2 movementDirection;
  public float movementSpeed = 5f;

  // Start is called before the first frame update
  void Start() {
    currentSpriteIndex = 0;
    if (currentAnimation.Count == 0)
      currentAnimation = spritesDown;
  }

  // Update is called once per frame
  void Update() {
    Move();
    if (movementDirection.magnitude > 0.0f && overrideAnimations == false) {
      UpdateAnimation();
    }

    if (mirror)
      charSpriteRenderer.transform.localScale = new Vector3(-1, 1, 1);
    else
      charSpriteRenderer.transform.localScale = new Vector3(1, 1, 1);

    if (overridePlayAnimation == false)
      PlayAnimation();
  }

  public void SetMovementVector(Vector2 movement) {
    movementDirection = movement;
  }

  private void Move() {
    Vector3 newPosition = transform.position + ((Vector3)movementDirection * (movementSpeed * Time.deltaTime));
    if (newPosition.x < GameManager.Instance.leftGameBound) {
      newPosition.x = GameManager.Instance.leftGameBound;
    } else if (newPosition.x > GameManager.Instance.rightGameBound) {
      newPosition.x = GameManager.Instance.rightGameBound;
    }

    if (newPosition.y > GameManager.Instance.upperGameBound) {
      newPosition.y = GameManager.Instance.upperGameBound;
    } else if (newPosition.y < GameManager.Instance.lowerGameBound) {
      newPosition.y = GameManager.Instance.lowerGameBound;
    }

    transform.position = newPosition;
  }

  private void UpdateAnimation() {
    if (Mathf.Abs(movementDirection.y) > Mathf.Abs(movementDirection.x)) {
      mirror = false;
      if (movementDirection.y > 0)
        currentAnimation = spritesUp;
      else
        currentAnimation = spritesDown;
    } else {
      if (movementDirection.x < 0) {
        mirror = false;
        currentAnimation = spritesHorizontal;
      } else {
        mirror = true;
        currentAnimation = spritesHorizontal;
      }
    }
  }

  private void PlayAnimation() {
    walkAnimTime += Time.deltaTime * walkAnimSpeed;
    if (walkAnimTime >= 1) {
      walkAnimTime = 0;
      currentSpriteIndex++;
    }

    if (currentSpriteIndex >= currentAnimation.Count || movementDirection.magnitude == 0)
      currentSpriteIndex = 0;

    charSpriteRenderer.sprite = currentAnimation[currentSpriteIndex];
  }
}
