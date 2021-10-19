using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingAI : BasicAI
{
    public List<Node> connections = new List<Node>();
    Node target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetFirstNode();
        connections.Add(next);

        target = Node.playerNode;
    }

    void Update()
    {
        target = Node.playerNode;
        if (Vector2.Distance(transform.position, next.transform.position) < 0.5)
        {
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
                        if (node == current.Connections[current.Connections.Count-1] && !hasAdded)
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
