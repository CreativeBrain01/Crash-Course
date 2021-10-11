using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] connections;

    private void OnDrawGizmos()
    {
        foreach(Node node in connections)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
}
