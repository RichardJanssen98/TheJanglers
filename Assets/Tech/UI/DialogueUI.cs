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
  private Vector3 hidePosition;

  private void Awake() {
  }

  public void StartDialogue(DialogueData dialogue) {
    currentDialogue = dialogue;
    currentDialogueIndex = 0;
    ShowDialogue(dialogue.dialogues[0]);
  }

  private void ShowDialogue(Dialogue dialogue) {
    leftCharacter.enabled = dialogue.characterPosition == DialogueCharacterPosition.Left;
    rightCharacter.enabled = dialogue.characterPosition == DialogueCharacterPosition.Right;
    Image charImg = leftCharacter.enabled ? leftCharacter : rightCharacter;

    if (leftCharacter.enabled)
      hidePosition = hiddenPosition;
    else if (rightCharacter.enabled)
      hidePosition = -hiddenPosition;

    transform.position = hidePosition;

    charImg.sprite = dialogue.characterSprite;
    text.text = dialogue.text;
    container.TweenPosition(showPosition, 1f).SetEaseBackOut().SetOnComplete(() => {
      audioSource.clip = dialogue.voice;
      audioSource.Play();
      container.TweenDelayedInvoke(dialogue.onScreenDuration, HideDialogue);
    });
  }

  private void HideDialogue() {
    container.TweenPosition(hidePosition, 1f).SetEaseBackIn().SetOnComplete(() => GoToNext());
  }

  public void Skip() {
    container.TweenCancelAll();
    audioSource.Stop();
    HideDialogue();
  }

  private void GoToNext() {
    Debug.Log(transform.position);
    currentDialogueIndex++;
    if (currentDialogueIndex == currentDialogue.dialogues.Count) {
      return;
    } else {
      ShowDialogue(currentDialogue.dialogues[currentDialogueIndex]);
    }
  }
}
