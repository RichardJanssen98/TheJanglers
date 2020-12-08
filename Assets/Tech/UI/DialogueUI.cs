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

  public System.Action onComplete;

  private void Awake() {
        audioSource.volume = PlayerPrefs.GetFloat("Options_AudioVolume");
  }

  public void StartDialogue(DialogueData dialogue) {
    currentDialogue = dialogue;
    currentDialogueIndex = 0;
    ShowDialogue(dialogue.dialogues[0]);
  }

  private void ShowDialogue(Dialogue dialogue, bool instant = false) {
    leftCharacter.enabled = dialogue.characterPosition == DialogueCharacter.Simon;
    rightCharacter.enabled = dialogue.characterPosition == DialogueCharacter.Lewis;
    Image charImg = leftCharacter.enabled ? leftCharacter : rightCharacter;

    if (leftCharacter.enabled)
      hidePosition = hiddenPosition;
    else if (rightCharacter.enabled)
      hidePosition = -hiddenPosition;

    transform.position = hidePosition;

    text.text = dialogue.text;

    if (instant) {
      container.transform.position = showPosition;
      audioSource.Play();
      container.TweenDelayedInvoke(dialogue.onScreenDuration, () => HideDialogue(dialogue));
    }


    container.TweenPosition(showPosition, 0.5f).SetEaseBackOut().SetOnComplete(() => {
      audioSource.clip = dialogue.voice;
      audioSource.Play();
      container.TweenDelayedInvoke(dialogue.onScreenDuration, () => HideDialogue(dialogue));
    });
  }

  private void HideDialogue(Dialogue dialogue) {
    if (dialogue.keepVisible) {
      GoToNext(true);
      return;
    }

    container.TweenPosition(hidePosition, 0.5f).SetEaseBackIn().SetOnComplete(() => GoToNext());
  }

  public void Skip() {
    container.TweenCancelAll();
    audioSource.Stop();
    HideDialogue(currentDialogue.dialogues[currentDialogueIndex]);
  }

  private void GoToNext(bool instant = false) {
    currentDialogueIndex++;
    if (currentDialogueIndex == currentDialogue.dialogues.Count) {
      onComplete?.Invoke();
      return;
    } else {
      ShowDialogue(currentDialogue.dialogues[currentDialogueIndex], instant);
    }
  }
}
