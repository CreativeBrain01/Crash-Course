using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicHandler : MonoBehaviour
{
    public static float masterVolume { get { return PlayerPrefs.GetFloat("MasterVolume", 1.0f); } }
    public static float musicVolume { get { return PlayerPrefs.GetFloat("MusicVolume", 0.5f); } }

    [SerializeField]
    AudioClip menuTheme;
    [SerializeField]
    AudioClip gameTheme;

    AudioSource audioSource;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = menuTheme;
        audioSource.Play();
    }

    string prevScene = "MainMenu";

    void Update()
    {
        float volume = masterVolume * musicVolume;

        audioSource.volume = volume;

        string currentScene = SceneManager.GetActiveScene().name;

        bool changeSong = false;
        if ((prevScene.Contains("Map") && !currentScene.Contains("Map")) || 
            (currentScene.Contains("Map") && !prevScene.Contains("Map")))
        {
            changeSong = true;
        }

        if (changeSong)
        {
            prevScene = currentScene;
            if (currentScene.Contains("Map"))
            {
                audioSource.Stop();
                audioSource.clip = gameTheme;
                audioSource.time = 0;
                audioSource.Play();
            }
            else
            {
                if (audioSource.clip != menuTheme)
                {
                    audioSource.Stop();
                    audioSource.clip = menuTheme;
                    audioSource.Play();
                }
            }
        }
    }
}