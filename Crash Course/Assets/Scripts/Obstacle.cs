using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : MonoBehaviour
{
    [SerializeField]
    int worth = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Movement>())
        {
            FindObjectOfType<GameController>().obstacleScore += worth;
            Destroy(transform.gameObject);
        }
    }
}
