using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataProcessing
{
    public static ScoreBoard scoreBoard;

    // Unlocks
    /*
       2010000
       0 = Locked[Requires Purchase]
       1 = Unlocked + UnEquipped
       2 = Unlocked + Equipped
    */

    public static void LoadData()
    {
        string scoreString = PlayerPrefs.GetString("ScoreBoard");

        if (!string.IsNullOrEmpty(scoreString.Trim()))
        {
            scoreBoard = JsonUtility.FromJson<ScoreBoard>(scoreString);
        } else
        {
            scoreBoard = new ScoreBoard();
            scoreBoard.scores.Add("CBT", 10600);
        }
    }
}
