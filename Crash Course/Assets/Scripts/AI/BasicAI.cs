using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicAI : MonoBehaviour
{
    protected Node next;
    protected Node previous;

    protected Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SelectVehicle();

        SetFirstNode();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, next.transform.position) > 0.5)
        {
            BasicMovement();
        }
        else
        {
            int index;
            int counter = 0;
            do
            {
                counter++;
                do
                {
                    index = Random.Range(0, next.Connections.Count);
                } while (next.Connections[index] == Node.playerNode);
            } while (next.Connections[index] == previous && counter < 400);
            previous = next;
            next = next.Connections[index];
        }
    }

    private void OnDrawGizmos()
    {
        if (next != null && this.isActiveAndEnabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, next.transform.position);
        }
    }

    protected void BasicMovement()
    {
        //Calculate Movement
        float x, y;
        Vector2 dir = new Vector2(next.transform.position.x - transform.position.x, next.transform.position.y - transform.position.y);
        x = Mathf.Clamp(dir.x, -1, 1);
        y = Mathf.Clamp(dir.y, -1, 1);

        //Move
        Vector2 movement = new Vector2(x, y) * 5;
        rb.velocity = movement;

        //Rotate
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected void SelectVehicle()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        int index = Random.Range(1, 7);

        switch (index)
        {
            case 1:
                sr.sprite = SpriteStorage.scooter;
                break;
            case 2:
                sr.sprite = SpriteStorage.quad;
                break;
            case 3:
                sr.sprite = SpriteStorage.motorcycle;
                break;
            case 4:
                sr.sprite = SpriteStorage.taxi;
                break;
            case 5:
                sr.sprite = SpriteStorage.bus;
                break;
            case 6:
                sr.sprite = SpriteStorage.truck;
                break;
            case 7:
                sr.sprite = SpriteStorage.camper;
                break;
            default:
                break;
        }
    }

    protected void SetFirstNode()
    {
        float shortDist = float.MaxValue;
        Node closestNode = null;
        Node[] nodes = FindObjectsOfType<Node>();
        foreach (Node node in nodes)
        {
            float dist = Vector2.Distance(transform.position, node.transform.position);
            if (dist < shortDist)
            {
                shortDist = dist;
                closestNode = node;
            }
        }
        if (closestNode != null) next = closestNode;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GetType() == typeof(ChasingAI))
            {
                collision.GetComponent<VehicleHandler>().TakeDamage();
            }
            else
            {
                GameController.Instance.obstacleScore += 100;
            }
            Destroy(this.gameObject);
        }
    }
}
