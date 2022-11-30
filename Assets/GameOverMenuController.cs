using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    //public static bool gameIsOver = false;

    //public bool playerDead;

    public GameObject gameOverMenuUI;

    //public GameObject pauseMenuUI;



    // Update is called once per frame
    void Awake()
    {
        //if (playerDead)
        //{
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                gameOverMenuUI.SetActive(true);
                //pauseMenuUI.SetActive(false);
        //}
        
    }

    //void PlayerDead()
    //{
   //     playerDead = true;
   // }

    public void LoadMenu()
    {
        gameOverMenuUI.SetActive(false);
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        //GameIsPaused = false;

        SceneManager.LoadScene(0);
        Debug.Log("Loading Menu..");

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game..");

    }
}
