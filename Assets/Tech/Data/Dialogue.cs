using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue {
  public Sprite characterSprite;
  public string text;
  public AudioClip voice;
  public float onScreenDuration;
  public DialogueCharacterPosition characterPosition;
}

public enum DialogueCharacterPosition {
  Left,
  Right
}
