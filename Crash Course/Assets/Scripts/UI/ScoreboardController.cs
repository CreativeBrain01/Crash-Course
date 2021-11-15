using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class ScoreboardController : MonoBehaviour
{
    [SerializeField]
    ScoreText[] scores;

    [SerializeField]
    GameObject inputArea;

    [SerializeField]
    TMP_InputField nameInput;

    bool hasInputed = false;

    public static ScoreBoard scoreBoard;

    private void Start()
    {
        string scoreString = PlayerPrefs.GetString("Scoreboard");

        if (!string.IsNullOrEmpty(scoreString))
        {
            //scoreBoard = JsonUtility.FromJson<ScoreBoard>(scoreString);
            scoreBoard = JsonConvert.DeserializeObject<ScoreBoard>(scoreString);
        }
        else
        {
            scoreBoard = new ScoreBoard();
            scoreBoard.scores.Add(10600, "CBT");
        }

        scoreBoard.scores.Capacity = 10;

        /*if (GameController.Instance != null)
        {
            if (GameController.Instance.Score != 0)
            {

            }
        }*/

        UpdateScoreboard();
    }


    void Update()
    {
        inputArea.SetActive(!hasInputed);
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].Rank = i + 1;
        }
    }

    private void UpdateScoreboard()
    {
        int c = scoreBoard.scores.Count;
        int highest = 0;

        List<int> s = new List<int>();
        List<string> n = new List<string>();
        //Dictionary<int, string> sb = new Dictionary<int, string>();

        for (int i = c-1; i > -1; i--)
        {
            s.Add(scoreBoard.scores.Keys[i]);
            n.Add(scoreBoard.scores.Values[i]);
            //sb.Add(scoreBoard.scores.Keys[i], scoreBoard.scores.Values[i]);
        }

        for (int i = 0; i < s.Count; i++)
        {
            highest = i;
            scores[i].Score = s[i];
            scores[i].Name = n[i];
        }

        /*
         scores[i].Score = scoreBoard.scores.Keys[i];
         scores[i].Name = scoreBoard.scores.Values[i];
         */

        if (highest < 10)
        {
            for (int i = highest + 1; i < 10; i++)
            {
                scores[i].Name = "";
                scores[i].Score = 0;
            }
        }

        string json = JsonConvert.SerializeObject(scoreBoard);
        //string json = JsonUtility.ToJson(scoreBoard);

        PlayerPrefs.SetString("Scoreboard", json);
        PlayerPrefs.Save();
    }

    public void InputData()
    {
        string ign = nameInput.text.ToUpper().Trim();

        if (string.IsNullOrEmpty(ign) || string.IsNullOrWhiteSpace(ign))
        {
            nameInput.text = "You didn't give any input.";
        } else if(ign.Length > 3)
        {
            nameInput.text = "Name Cannot be more than 3 characters";
        } else
        {
            bool hasInvalidChar = false;
            foreach (char c in ign)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    hasInvalidChar = true;
                    break;
                }
            }

            if (hasInvalidChar)
            {
                nameInput.text = "Name must only contain letters and numbers.";
            } else
            {
                scoreBoard.scores.Add(GameController.Instance.Score, ign);
                hasInputed = true;

                UpdateScoreboard();
            }
        }
    }
}