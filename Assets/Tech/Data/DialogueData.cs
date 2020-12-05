using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Santa Sleigher/Dialogue Data")]
public class DialogueData : ScriptableObject {
  public List<Dialogue> dialogues = new List<Dialogue>();
}
