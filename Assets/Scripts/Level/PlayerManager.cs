using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{

    public static bool gameOver;
    public GameObject gameOverPanel;

    public GameObject pauseButton;

    public static bool isGameStarted;

    public GameObject startingText;

    public static int numberOfCoins = 0;

    public Text coinText;

    public static bool isCoinUpdated = false;

    public Animator gameOverPanelAnim;

    bool callAnimOnce = false;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        numberOfCoins = 0;
        AdManager.instance.RequestInterstitial();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
            pauseButton.SetActive(false);
            Time.timeScale = 0;
            if (!isCoinUpdated)
            {
                PlayerPrefs.SetInt("NumberOfCoins", PlayerPrefs.GetInt("NumberOfCoins", 0) + numberOfCoins);
                if (PlayerPrefs.GetInt("HighScore") < numberOfCoins)
                    PlayerPrefs.SetInt("HighScore", numberOfCoins);



                AdManager.instance.ShowInterstitial();




            }

            isCoinUpdated = true;

        }

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
        }

        coinText.text = numberOfCoins + "";
    }

    void LateUpdate()
    {

        if (gameOverPanelAnim.gameObject.activeSelf && !callAnimOnce)
        {
            gameOverPanelAnim.SetTrigger("isGameOver");
            callAnimOnce = true;
        }

    }

}
