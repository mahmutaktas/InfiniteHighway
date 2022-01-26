using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Events : MonoBehaviour
{

    public GameObject pauseGamePanel;
    public GameObject pauseButton;
    public Animator pauseAnimator;

    public void Restart()
    {
        PlayerManager.isCoinUpdated = false;
        SceneManager.LoadScene("Level");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        PlayerManager.isCoinUpdated = false;
        SceneManager.LoadScene("Menu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseGamePanel.SetActive(true);
        pauseButton.SetActive(false);
        pauseAnimator.SetTrigger("isPauseMenu");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseGamePanel.SetActive(false);
        pauseButton.SetActive(true);
    }


}
