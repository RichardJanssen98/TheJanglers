using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using ElRaccoone.Tweens;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class Button : MonoBehaviour {

  public UnityEngine.UI.Button button;
  public UnityEvent onClick;

  public static bool interactible;

  // Start is called before the first frame update
  void Start() {
    button = GetComponent<UnityEngine.UI.Button>();
    button.onClick.AddListener(OnClick);
  }

  void OnClick() {
    interactible = false;
    gameObject.TweenLocalScale(Vector3.one * 0.75f, 0.1f)
      .SetFrom(Vector3.one)
      .SetOnComplete(() => {
        gameObject.TweenLocalScale(Vector3.one, 0.1f).SetOnComplete(() => onClick.Invoke());
      });
  }

}
