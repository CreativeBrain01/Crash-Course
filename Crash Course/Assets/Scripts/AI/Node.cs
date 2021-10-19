using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    public bool isAttatchedToPlayer = false;
    [SerializeField]
    List<Node> staticConnections = new List<Node>();

    float detectionDist = 2;
    bool inPlayerRange { 
        get 
        { 
            if (!isAttatchedToPlayer)
            {
                bool isInrange = Vector2.Distance(transform.position, playerNode.transform.position) <= detectionDist;
                return isInrange;
            } else
            {
                return false;
            }
        } 
    }
    public List<Node> Connections { 
        get {
            List<Node> returnList = new List<Node>();
            returnList.AddRange(staticConnections);
            if (inPlayerRange)
            {
                returnList.Add(playerNode);
            }
            return returnList;
        } 
    }

    public static Node playerNode;

    private void Start()
    {
        if (isAttatchedToPlayer)
        {
            playerNode = this;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (isAttatchedToPlayer)
        {
            playerNode = this;
        } else
        {
            if (inPlayerRange)
            {
                Gizmos.DrawLine(transform.position, playerNode.transform.position);
            }
        }
        foreach (Node node in staticConnections)
        {
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
}
