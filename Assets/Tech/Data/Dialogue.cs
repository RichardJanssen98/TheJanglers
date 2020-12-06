using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {
  public string text;
  public AudioClip voice;
  public float onScreenDuration;
  public DialogueCharacter characterPosition;
  public bool keepVisible;
}

public enum DialogueCharacter {
  Lewis,
  Simon
}
