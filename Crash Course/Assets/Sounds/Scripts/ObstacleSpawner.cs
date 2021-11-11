using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] obstacles;

    static int maxObstacles = 4;
    static float spawnDist = 0.5f;
    static float respawnTime = 10f;


    float respawnTimer = 0.0f;
    private void Update()
    {
        if (transform.childCount <= 0 && obstacles.Length > 0)
        {
            if (respawnTimer <= respawnTime)
            {
                respawnTimer += Time.deltaTime;
            } else {
                respawnTimer = 0;
                int count = Random.Range(1, maxObstacles + 1);
                GameObject chosenObstacle = obstacles[Random.Range(0, obstacles.Length)];

                for (int i = 0; i < count; i++)
                {
                    GameObject obstacle = Instantiate(chosenObstacle, transform.position, transform.rotation, transform);

                    float x = Random.Range(-spawnDist, spawnDist); float y = Random.Range(-spawnDist, spawnDist);
                    Vector3 v = new Vector3(x, y, 0);

                    obstacle.transform.position += v;
                }
            }
        }
    }
}
