using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ReturnToMainMenu : MonoBehaviour
{
        public void LoadMenu()
    {

        SceneManager.LoadScene(0);
        Debug.Log("Loading Menu..");

    }

}
