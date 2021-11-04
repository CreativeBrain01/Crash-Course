using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    [SerializeField]
    int minEnemies = 5;
    [SerializeField]
    GameObject enemyParent;
    [SerializeField]
    GameObject policeSpawn;

    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject targetPrefab;
    [SerializeField]
    GameObject policePrefab;

    public int targetCount { get { return GameObject.FindGameObjectWithTag("Target") ? 1 : 0; } }

    List<Node> validNodes = new List<Node>();
    

    private void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount <= minEnemies)
        {
            validNodes.Clear();

            foreach (Node node in FindObjectsOfType<Node>())
            {
                if (node.Connections.Contains(Node.playerNode) || node == Node.playerNode)
                {
                    continue;
                } else
                {
                    validNodes.Add(node);
                }
            }

            for (int i = 0; i < validNodes.Count * 0.6; i++)
            {
                int index = Random.Range(0, validNodes.Count);

                Instantiate(enemyPrefab, validNodes[index].transform.position, validNodes[index].transform.rotation, enemyParent.transform);
            }
        }

        if (targetCount <= 0)
        {
            ObstacleSpawner[] spawners = FindObjectsOfType<ObstacleSpawner>();

            int index;
            do
            {
                index = Random.Range(0, spawners.Length);
            } while (spawners[index] != null && Vector2.Distance(spawners[index].transform.position, Node.playerNode.transform.position) <= 1);

            ObstacleSpawner spawner = spawners[index];

            Instantiate(targetPrefab, spawner.transform.position, spawner.transform.rotation, enemyParent.transform);
        }

        if (GameController.Instance.Score >= 1000 && FindObjectsOfType<ChasingAI>().Length <= 0)
        {
            Instantiate(policePrefab, policeSpawn.transform.position, policeSpawn.transform.rotation, policeSpawn.transform);
        }
    }
}
