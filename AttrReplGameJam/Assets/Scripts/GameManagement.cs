using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float countdownInterval;
    [SerializeField] private List<string> countdownStrings;

    [Header("Object References")]
    [SerializeField] private GameObject player;
    [SerializeField] private EnemyManagement enemyManager;
    
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI countdownUI;
    [SerializeField] private TextMeshProUGUI pointUI;
    [SerializeField] private PauseMenu pauseMenu;

    [Header("Animator References")]
    [SerializeField] private Animator countdownUIAnimator;
    [SerializeField] private Animator pointUIAnimator;

    private bool isCountingDown;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to update UI when points added
        Actions.PointsAdded += AddPointsUI;

        // Start countdown
        StartCoroutine(Countdown());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            Pause();
    }

    // Start Game
    private void StartGame() 
    {
        player.GetComponent<PlayerInput>().EnableInput();
        enemyManager.EnableSpawning();
        pointUI.transform.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    // ======== PAUSING ========

    public void Pause() 
    {
        if (isCountingDown) return;

        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            enemyManager.DisableSpawning();
            player.GetComponent<PlayerInput>().DisableInput();
            pauseMenu.EnablePauseMenu(true);
        }
        else
        {
            pauseMenu.EnablePauseMenu(false);
            StartCoroutine(Countdown());
        }
    }

    // ======== UI FUNCTIONS ========

    // Countdown for UI
    IEnumerator Countdown() 
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
        StartGame();
    }

    // Add points to UI
    private void AddPointsUI() 
    {
        pointUI.text = "" + player.GetComponent<PlayerPoints>().GetPoints();
        pointUIAnimator.Play("TextChange");
    }
}
