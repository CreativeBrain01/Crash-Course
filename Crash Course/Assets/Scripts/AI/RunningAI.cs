using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAI : BasicAI
{
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetFirstNode();
    }

    public List<Node> connections = new List<Node>();
    bool foundTarget = false;
    int counter = 0;
    void Update()
    {
        if (!foundTarget)
        {
            #region find the node farthest from the player
            Node target = null;
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

            #region find connection that isnt either the player or the one it was already going to
            Node current = null;
            for (int i = 0; i < next.Connections.Count; i++)
            {
                if (next.Connections[i] != Node.playerNode && next.Connections[i] != previous)
                {
                    current = next.Connections[i];
                    break;
                }
            }
            #endregion

            #region create list of points that need to be traveled to
            Node prev2 = next;

            float infiniteSafety1 = 0;
            do
            {
                infiniteSafety1++;
                float infinitySafety2 = 0;
                do
                {
                    infinitySafety2++;
                    for (int i = 0; i < current.Connections.Count; i++)
                    {
                        if (current.Connections[i] == target)
                        {
                            connections.Add(current);
                            foundTarget = true;
                            break;
                        } else if (current.Connections[i] != Node.playerNode && current.Connections[i] != previous && !connections.Contains(current.Connections[i]))
                        {
                            connections.Add(current);
                            current = current.Connections[i];
                            continue;
                        }
                    }
                } while (current == previous || connections.Contains(current) && !foundTarget && infinitySafety2 < 200);
            } while (!foundTarget && infiniteSafety1 < 200);
            next = connections[0];
            #endregion
        } else
        {
            if (Vector2.Distance(transform.position, connections[counter].transform.position) > 0.5)
            {
                BasicMovement();
            } else
            {
                counter++;
                if (counter >= connections.Count)
                {   
                    counter = 0;
                    //foundTarget = false;
                    GetComponent<BasicAI>().enabled = true;
                    this.enabled = false;
                }
                previous = next;
                next = connections[counter];
            }
        }
    }
}
