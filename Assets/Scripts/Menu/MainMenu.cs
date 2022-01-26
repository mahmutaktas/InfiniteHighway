using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public GameObject[] menuItems;

    public GameObject[] shopMenuItems;

    public TextMeshProUGUI coinText;

    public Text highScoreText;

    public Animator shopPanelAnim;

    public Animator mainMenuAnim;

    void Start()
    {
        coinText.text = PlayerPrefs.GetInt("NumberOfCoins", 0) + "";
        highScoreText.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore", 0);
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Shop()
    {

        foreach(var item in menuItems)
        {
            item.SetActive(false);
        }
        foreach(var item in shopMenuItems)
        {
            item.SetActive(true);
        }

        shopPanelAnim.SetTrigger("ShopPanel");

    }

    public void BackToMenu()
    {
        foreach (var item in menuItems)
        {
            item.SetActive(true);
        }
        foreach (var item in shopMenuItems)
        {
            item.SetActive(false);
        }

        mainMenuAnim.SetTrigger("MainMenu");
    }


}
