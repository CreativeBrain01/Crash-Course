using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    TMP_Text rank_Txt;
    [SerializeField]
    TMP_Text name_Txt;
    [SerializeField]
    TMP_Text score_Txt;

    public int Rank { get; set; } = 0;
    public string Name { get; set; } = "BoB";
    public int Score { get; set; } = 0;

    void Update()
    {
        rank_Txt.text = Rank.ToString();
        name_Txt.text = Name;
        score_Txt.text = Score.ToString();
    }
}
