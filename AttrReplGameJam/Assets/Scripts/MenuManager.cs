using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameManagement gameManagement;
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private GameObject PauseMenu;

    private void Start()
    {
        Actions.OnPlayerDeath += EnableGameOverMenu;
    }

    // ===== PAUSE MENU =====

    public void EnablePauseMenu(bool enable)
    {
        PauseMenu.SetActive(enable);
    }

    public void ResumeGameBtn()
    {
        gameManagement.Pause();
    }

    // ===== GAME OVER MENU =====

    private void EnableGameOverMenu()
    {
        GameOverMenu.SetActive(true);
    }

    public void RestartGameBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    // ===== OTHER MENU OPTIONS =====

    public void MenuBtn()
    {

    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    void OnDestroy()
    {
        Actions.OnPlayerDeath -= EnableGameOverMenu;
    }
}
