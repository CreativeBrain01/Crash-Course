using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField]
    GameObject musicHandler;

    int MapCount = 1;

    public void Start()
    {
        if (!FindObjectOfType<MusicHandler>() && musicHandler != null)
        {
            Instantiate(musicHandler, null, true);
        }

        if (SceneManager.GetActiveScene().name == "Settings")
        {
            GameObject.FindGameObjectWithTag("MasterVol").GetComponent<Slider>().value = MusicHandler.masterVolume;
            GameObject.FindGameObjectWithTag("MusicVol").GetComponent<Slider>().value = MusicHandler.musicVolume;
            GameObject.FindGameObjectWithTag("SFXVol").GetComponent<Slider>().value = MusicHandler.sfxVolume;
            GameObject.FindGameObjectWithTag("ScoreSkip").GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("UseScores", 1) == 1;
        }
    }

    public void Play()
    {
        if (PlayerPrefs.GetInt("ViewedInstructions") < 1)
        {
            PlayerPrefs.SetInt("ViewedInstructions", 2);
            Instructions();
        }
        else
        {
            GoToMap();
        }
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Instructions()
    {
        if (PlayerPrefs.GetInt("ViewdInstructions") != 2) PlayerPrefs.SetInt("ViewedInstructions", 1);
        SceneManager.LoadScene("Instructions");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Scoreboard()
    {
        SceneManager.LoadScene("Scoreboard");
    }

    public void Exit()
    {
        PlayerPrefs.Save();

        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            Application.Quit();
        }
        else if (SceneManager.GetActiveScene().name == "Instructions" && PlayerPrefs.GetInt("ViewedInstructions") == 2)
        {
            GoToMap();
        }
        else
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void MastVol(float vol)
    {
        PlayerPrefs.SetFloat("MasterVolume", vol);
    }

    public void MusicVol(float vol)
    {
        PlayerPrefs.SetFloat("MusicVolume", vol);
    }

    public void SFXVol(float vol)
    {
        PlayerPrefs.SetFloat("SFXVolume", vol);
    }

    public void ScoreSkip(bool skip)
    {
        PlayerPrefs.SetInt("UseScores", skip ? 0 : 1);
    }

    private void GoToMap()
    {
        int mapNum = Random.Range(1, MapCount);

        SceneManager.LoadScene("Map" + mapNum);
    }
}
