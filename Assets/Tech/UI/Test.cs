using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
  public DialogueData dialogue;

  // Start is called before the first frame update
  void Start() {
    Invoke("StartDialogue", 1);
  }

  // Update is called once per frame
  void Update() {

  }

  void StartDialogue() {
    DialogueUI.Instance.StartDialogue(dialogue);
  }
}
