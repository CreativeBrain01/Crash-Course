using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// timerStart describes the value the timer will start at.
    /// </summary>
    [SerializeField]
    float timerStart = 120f;
    [SerializeField]
    TMP_Text timerTxt;
    [SerializeField]
    TMP_Text scoreTxt;

    float timer;

    public void AddTime(float t)
    {
        timer += t;
    }

    //this value is used to help determine score.
    float stopWatch = 0;
    
    public int obstacleScore = 0;

    public int Score { get { return (Mathf.RoundToInt(stopWatch) * 10) + obstacleScore; } }

    public bool gameOver = false;

    public static GameController Instance;

    void Start()
    {
        timer = timerStart;
        Instance = this;
    }

    void Update()
    {
        if (!gameOver)
        {
            timerTxt.text = "Time: " + Mathf.RoundToInt(timer);
            scoreTxt.text = "Score: " + Score;
            if (timer > 0)
            {
                stopWatch += Time.deltaTime;
                timer -= Time.deltaTime;
            } else
            {
                gameOver = true;
            }
        } else
        {
            PlayerPrefs.SetInt("HubCaps", PlayerPrefs.GetInt("HubCaps") + (Score / 100));
            SceneManager.LoadScene("Main Menu");
        }
    }
}
