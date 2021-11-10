using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicHandler : MonoBehaviour
{
    public float musicVolume { get { return PlayerPrefs.GetFloat("MusicVolume", 0.5f); } }
    public float sfxVolume { get { return PlayerPrefs.GetFloat("SFXVolume", 0.5f); } }
    public float masterVolume { get { return PlayerPrefs.GetFloat("MasterVolume", 1.0f); } }

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
    }

    string prevScene = "MainMenu";
    void Update()
    {
        float volume = masterVolume * musicVolume;

        audioSource.volume = volume;

        string currentScene = SceneManager.GetActiveScene().name;

        if (prevScene != currentScene)
        {
            audioSource.Stop();

            prevScene = currentScene;
            if (currentScene.Contains("Map"))
            {
                audioSource.clip = gameTheme;
                audioSource.time = 0;
            }
            else
            {
                if (audioSource.clip != menuTheme) audioSource.clip = menuTheme;
            }

            audioSource.Play();
        }
    }
}
