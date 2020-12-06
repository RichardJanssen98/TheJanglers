using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

  public float baseDifficulty = 1;
  public float maxDifficulty = 1.5f;
  public float difficulty = 1;

  public float leftGameBound = -9;
  public float rightGameBound = 9;
  public float upperGameBound = 9;
  public float lowerGameBound = -9;

  private const float difficultyStep = 0.1f;

  public bool gameFrozen = true;

  public List<DialogueData> dialogues = new List<DialogueData>();

  private void Awake() {
    gameFrozen = true;
    Player.Instance.movement.overrideAnimations = true;
  }

  public void StartIntro() {
    gameFrozen = true;
    int randomIndex = Random.Range(0, dialogues.Count);
    DialogueUI.Instance.onComplete = StartGame;
    DialogueUI.Instance.StartDialogue(dialogues[randomIndex]);
  }

  public void StartGame() {
    difficulty = baseDifficulty;
    gameFrozen = false;
    Player.Instance.movement.overrideAnimations = false;
  }

  public void IncreaseDifficulty() {
    difficulty += difficultyStep;
    AIBehaviour.UpdateAllAIDifficulties();
  }

  private void Update() {

  }
}
