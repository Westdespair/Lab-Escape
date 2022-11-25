using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public static PauseMenuController instance;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Resume();
            }
            else if(!GameIsPaused)
            {
                        Time.timeScale = 1f;
                        GameIsPaused = false;

                Cursor.lockState = CursorLockMode.None;
                Pause();

            }
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
              DontDestroyOnLoad(gameObject);
        }

      else
        {
            Destroy(gameObject);
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        SceneManager.LoadScene(0);
        Debug.Log("Loading Menu..");

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game..");

    }
}
