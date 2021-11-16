using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private void Start()
    {
        Actions.OnPlayerDeath += EnableGameOverMenu;
    }

    private void EnableGameOverMenu()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void RestartGameBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void MenuBtn()
    {

    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
