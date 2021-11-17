using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    private void Update()
    {
        foreach (BasicAI basicAI in FindObjectsOfType<BasicAI>())
        {
            basicAI.speed = 2;
        }
    }
}
