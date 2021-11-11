using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class CashDisplay : MonoBehaviour
{
    TMP_Text textBox;

    private void Start()
    {
        textBox = GetComponent<TMP_Text>();
    }

    void Update()
    {
        textBox.text = "$ " + PlayerPrefs.GetInt("HubCaps");
    }
}
