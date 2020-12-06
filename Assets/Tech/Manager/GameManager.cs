using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

  public float baseDifficulty = 1;
  public float maxDifficulty = 1.5f;
  public float difficulty = 1;

  private const float difficultyStep = 0.1f;

  public bool gameFrozen = false;

  public void StartGame() {
    difficulty = baseDifficulty;
  }

  public void IncreaseDifficulty() {
    difficulty += difficultyStep;
    AIBehaviour.UpdateAllAIDifficulties();
  }

  private void Update() {

  }
}
