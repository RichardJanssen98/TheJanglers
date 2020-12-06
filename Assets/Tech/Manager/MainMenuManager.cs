using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ElRaccoone.Tweens;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public GameObject settingsPanel;
    public Slider audioVolumeSlider;
    public Text audioVolumeText;

  public void StartGame() {
    SceneManager.LoadScene(1);
  }

  public void QuitGame() {
    Application.Quit();
  }

    private void Update()
    {
        audioVolumeText.text = audioVolumeSlider.value + "%";
        PlayerPrefs.SetFloat("Options_AudioVolume", audioVolumeSlider.value / 100f);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        settingsPanel.TweenLocalScale(Vector3.one, 0.25f).SetFrom(Vector3.zero).SetEaseBackOut();
    }

    public void CloseSettings()
    {
        settingsPanel.TweenLocalScale(Vector3.zero, 0.25f).SetFrom(Vector3.one).SetOnComplete(() => settingsPanel.SetActive(false));
    }
}
