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
        if (!FindObjectOfType<MusicHandler>())
        {
            Instantiate(musicHandler, null, true);
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

    private void GoToMap()
    {
        int mapNum = Random.Range(1, MapCount);

        SceneManager.LoadScene("Map" + mapNum);
    }
}
