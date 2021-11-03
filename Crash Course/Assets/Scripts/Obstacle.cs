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
            GameController.Instance.obstacleScore += worth;
            collision.attachedRigidbody.velocity = new Vector3(0, 0, 0);
            Destroy(transform.gameObject);
        }
    }
}
