using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElRaccoone.Tweens;

public class GameManager : Singleton<GameManager>
{

    public float baseDifficulty = 1;
    public float maxDifficulty = 1.5f;
    public float difficulty = 1;

    public int baseMaxElves = 2;
    public int maxElves = 2;
    public float spawnInterval = 6;
    private float spawnTimer = 0;
    private int spawnIndex;

    public float leftGameBound = -9;
    public float rightGameBound = 9;
    public float upperGameBound = 9;
    public float lowerGameBound = -9;

    private const float difficultyStep = 0.1f;

    public bool gameFrozen = true;

    public List<Transform> elfSpawnPositions = new List<Transform>();

    public List<DialogueData> dialogues = new List<DialogueData>();

    public List<AudioClip> gameOverLaughs = new List<AudioClip>();

    public AudioSource audioSource;

    public FollowPlayerBehaviour elfPrefab;

    public GameObject playerHealthBar;
    public GameObject bossHealthBar;

    public GameObject gameOverPanel;
    public GameObject gameCompletedPanel;

    List<FollowPlayerBehaviour> elves = new List<FollowPlayerBehaviour>();

    private void Awake()
    {
        gameFrozen = true;
        BackgroundMusicController.Instance.PlayIntroSoundtrack();
        Player.Instance.movement.overrideAnimations = true;
        gameOverPanel.transform.localScale = Vector3.zero;
        gameCompletedPanel.transform.localScale = Vector3.zero;
        playerHealthBar.transform.localScale = Vector3.zero;
        bossHealthBar.transform.localScale = Vector3.zero;
        elves.AddRange(FindObjectsOfType<FollowPlayerBehaviour>());
    }

    public void StartIntro()
    {
        gameFrozen = true;
        int randomIndex = Random.Range(0, dialogues.Count);
        DialogueUI.Instance.onComplete = StartGame;
        DialogueUI.Instance.StartDialogue(dialogues[randomIndex]);
    }

    public void StartGame()
    {
        BackgroundMusicController.Instance.PlayFightSoundtrack();
        playerHealthBar.TweenLocalScale(Vector3.one, 0.5f).SetOnComplete(() =>
        {
            difficulty = baseDifficulty;
            gameFrozen = false;
            Player.Instance.movement.overrideAnimations = false;
        });
        bossHealthBar.TweenLocalScale(Vector3.one, 0.5f);
    }

    public void IncreaseDifficulty()
    {
        difficulty += difficultyStep;
        AIBehaviour.UpdateAllAIDifficulties();
        maxElves += 2;
        spawnInterval--;
    }

    public void GameOver()
    {
        int random = Random.Range(0, gameOverLaughs.Count);
        AudioClip laff = gameOverLaughs[random];
        audioSource.clip = laff;
        audioSource.Play();
        gameFrozen = true;
        HideBars();
        gameOverPanel.TweenLocalScale(Vector3.one, 0.5f).SetEaseBackOut();
    }

    public void CompleteGame()
    {
        gameFrozen = true;
        HideBars();
        gameCompletedPanel.TweenLocalScale(Vector3.one, 0.5f).SetEaseBackOut();
    }

    private void Update()
    {
        if (gameFrozen)
            return;

        if (elves.Count < maxElves)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                FollowPlayerBehaviour elf = (FollowPlayerBehaviour)Instantiate(elfPrefab);
                elf.transform.position = elfSpawnPositions[spawnIndex].position;
                spawnIndex++;
                if (spawnIndex > elfSpawnPositions.Count - 1)
                    spawnIndex = 0;
                spawnTimer = 0.0f;
            }
        }
    }

    private void HideBars()
    {
        playerHealthBar.TweenLocalScale(Vector3.zero, 0.5f);
        bossHealthBar.TweenLocalScale(Vector3.zero, 0.5f);
    }

    public void GoToMenu()
    {
        playerHealthBar.GetComponent<Healthbar>().Deinitialize();
        bossHealthBar.GetComponent<Healthbar>().Deinitialize();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        playerHealthBar.GetComponent<Healthbar>().Deinitialize();
        bossHealthBar.GetComponent<Healthbar>().Deinitialize();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
