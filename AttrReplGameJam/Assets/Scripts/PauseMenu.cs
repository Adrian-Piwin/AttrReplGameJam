using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameManagement gameManagement;

    public void EnablePauseMenu(bool enable)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(enable);
        }
    }

    public void ResumeGameBtn()
    {
        gameManagement.Pause();
    }

    public void SettingsBtn()
    {

    }

    public void MenuBtn()
    {

    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
