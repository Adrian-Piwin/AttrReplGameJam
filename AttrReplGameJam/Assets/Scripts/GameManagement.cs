using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float endGameSlowmo;
    [SerializeField] private float countdownInterval;
    [SerializeField] private List<string> countdownStrings;

    [Header("Object References")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private EnemyManagement enemyManager;
    [SerializeField] private SoundManagement soundManager;
    
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI countdownUI;
    [SerializeField] private TextMeshProUGUI pointUI;
    [SerializeField] private MenuManager menuManager;

    [Header("Animator References")]
    [SerializeField] private Animator countdownUIAnimator;
    [SerializeField] private Animator pointUIAnimator;

    private GameObject player;
    private bool isCountingDown;
    private bool isPaused;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        Actions.PointsAdded += AddPointsUI;
        Actions.OnPlayerDeath += EndGame;

        // Start countdown
        StartCoroutine(Countdown("InitalStart"));
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            Pause();
    }

    // Inital start of game
    private void StartGame() 
    {
        player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);

        enemyManager.EnableSpawning();
        pointUI.transform.gameObject.SetActive(true);
    }

    // End game
    private void EndGame() 
    {
        Time.timeScale = endGameSlowmo;
        enemyManager.DisableSpawning();
        soundManager.StopAllSoundEffects();
        gameOver = true;
    }

    // ======== PAUSING ========

    // Unpausing game
    private void UnpauseGame()
    {
        player.GetComponent<PlayerInput>().EnableInput();
        enemyManager.EnableSpawning();
        Time.timeScale = 1;
    }

    // Pause game
    private void PauseGame()
    {
        player.GetComponent<PlayerInput>().DisableInput();
        enemyManager.DisableSpawning();
        soundManager.StopAllSoundEffects();
        Time.timeScale = 0;
    }

    // Toggle pausing game
    public void Pause() 
    {
        if (isCountingDown || gameOver) return;

        isPaused = !isPaused;
        if (isPaused)
        {
            menuManager.EnablePauseMenu(true);
            PauseGame();
        }
        else
        {
            menuManager.EnablePauseMenu(false);
            StartCoroutine(Countdown("UnpauseStart"));
        }
    }

    // ======== UI FUNCTIONS ========

    // Countdown for UI
    IEnumerator Countdown(string startType) 
    {
        isCountingDown = true;

        foreach (string str in countdownStrings) 
        {
            yield return new WaitForSecondsRealtime(countdownInterval);
            countdownUIAnimator.Play("TextChange");
            countdownUI.text = "" + str;
        }
        yield return new WaitForSecondsRealtime(countdownInterval);
        countdownUI.text = "";

        isCountingDown = false;

        if (startType == "InitalStart")
            StartGame();
        else if (startType == "UnpauseStart")
            UnpauseGame();
    }

    // Add points to UI
    private void AddPointsUI(int points) 
    {
        pointUI.text = "" + points;
        pointUIAnimator.Play("TextChange");
    }

    void OnDestroy() 
    {
        Actions.PointsAdded -= AddPointsUI;
        Actions.OnPlayerDeath -= EndGame;
    }
}
