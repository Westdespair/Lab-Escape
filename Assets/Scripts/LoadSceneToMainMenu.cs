using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneToMainMenu : MonoBehaviour
{

    [Header("Settings")]
    public string nameOfSceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { SceneManager.LoadScene(nameOfSceneToLoad); }
    }

}
