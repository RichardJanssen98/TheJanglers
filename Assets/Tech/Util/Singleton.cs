using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component {
  protected static T _instance;

  public static T Instance {
    get {
      if (_instance == null) {
        _instance = GameObject.FindObjectOfType<T>();

        if (_instance == null) {
          GameObject container = new GameObject(typeof(T).ToString());
          _instance = container.AddComponent<T>();
        }
      }
      return _instance;
    }
  }
}
