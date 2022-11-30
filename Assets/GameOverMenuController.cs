using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    //public static bool gameIsOver = false;

    public static bool playerDead = false;

    public GameObject gameOverMenuUI;


    // Update is called once per frame
    void Update()
    {
        if (playerDead)
        {
                Cursor.lockState = CursorLockMode.Locked;
                Pause();
        }
        
    }

    void Pause()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        //GameIsPaused = true;
    }

    public void LoadMenu()
    {
        gameOverMenuUI.SetActive(false);
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
