using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    [SerializeField]
    int minEnemies = 5;
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject enemyParent;

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
    }
}
