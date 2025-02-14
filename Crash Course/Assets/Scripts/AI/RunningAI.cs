using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAI : BasicAI
{
    public List<Node> connections = new List<Node>();
    Node target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SelectVehicle();

        SetFirstNode();
        connections.Add(next);
    }

    
    void Update()
    {
        float d = Vector2.Distance(transform.position, next.transform.position);
        if (d < 0.5)
        {
            #region find the node farthest from the player

            float maxDist = float.MinValue;
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            foreach (Node node in FindObjectsOfType<Node>())
            {
                float dist = Vector2.Distance(player.transform.position, node.transform.position);
                if (dist > maxDist)
                {
                    maxDist = dist;
                    target = node;
                }
            }
            #endregion

            bool nextIsTarget = false;
            bool foundTarget = false;
            Node current = next;
            List<Node> temp = new List<Node>();

            int overflowSafety = 0;
            do
            {
                overflowSafety++;
                bool hasAdded = false;
                foreach (Node node in current.Connections)
                {
                    if (node == previous)
                    {
                        continue;
                    }
                    else if (node == target)
                    {
                        if (temp.Count <= 0)
                        {
                            nextIsTarget = true;
                            foundTarget = true;
                        }
                        else
                        {
                            foundTarget = true;
                            temp.Add(target);
                            break;
                        }
                    }
                    else if
                          (Vector2.Distance(node.transform.position, target.transform.position)
                          <=
                          Vector2.Distance(current.transform.position, target.transform.position))
                    {
                        temp.Add(node);
                        hasAdded = true;
                        current = node;
                        break;
                    }
                    else
                    {
                        if (node == current.Connections[current.Connections.Count - 1] && !hasAdded)
                        {
                            temp.Add(node);
                        }
                    }
                }
            } while (!foundTarget && overflowSafety < 200);

            if (!nextIsTarget)
            {
                connections = temp;
            }
            else
            {
                connections.Clear();
                connections.Add(target);
            }
        }
        else
        {
            next = connections[0];
            BasicMovement();
        }
    }
}
