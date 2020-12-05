using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ElRaccoone.Tweens;

public class DialogueUI : Singleton<DialogueUI> {

  public Transform container;
  public Image leftCharacter;
  public Image rightCharacter;
  public Text text;
  public AudioSource audioSource;

  public Vector3 hiddenPosition;
  public Vector3 showPosition;

  private DialogueData currentDialogue;
  private int currentDialogueIndex;

  public void StartDialogue(DialogueData dialogue) {
    currentDialogue = dialogue;
    currentDialogueIndex = 0;
    ShowDialogue(dialogue.dialogues[0]);
  }

  private void ShowDialogue(Dialogue dialogue) {
    leftCharacter.enabled = dialogue.characterPosition == DialogueCharacterPosition.Left;
    rightCharacter.enabled = dialogue.characterPosition == DialogueCharacterPosition.Right;
    Image charImg = leftCharacter.enabled ? leftCharacter : rightCharacter;
    charImg.sprite = dialogue.characterSprite;
    text.text = dialogue.text;
    container.TweenPosition(showPosition, 0.5f).SetEaseBackInOut().SetOnComplete(() => {
      audioSource.clip = dialogue.voice;
      audioSource.Play();
      container.TweenDelayedInvoke(dialogue.onScreenDuration, HideDialogue);
    });
  }

  private void HideDialogue() {
    container.TweenPosition(hiddenPosition, 0.5f).SetEaseBackInOut().SetOnComplete(() => GoToNext());
  }

  public void Skip() {
    container.TweenCancelAll();
    audioSource.Stop();
    HideDialogue();
  }

  private void GoToNext() {
    currentDialogueIndex++;
    if (currentDialogueIndex == currentDialogue.dialogues.Count) {
      return;
    } else {
      ShowDialogue(currentDialogue.dialogues[currentDialogueIndex]);
    }
  }
}
