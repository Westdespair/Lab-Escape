using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource actionSource;
    [SerializeField] List<AudioClip> actionClips = new List<AudioClip>();

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


    public void ActionSFX()
    {
    AudioClip clip = actionClips[Random.Range(0, actionClips.Count)];
    actionSource.PlayOneShot(clip);
    }

}
