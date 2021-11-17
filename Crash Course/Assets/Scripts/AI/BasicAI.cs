using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicAI : MonoBehaviour
{
    protected Node next;
    protected Node previous;

    protected Rigidbody2D rb;

    protected VehicleHandler.eVehicle vehicle;

    public float speed = 3;

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
        Vector2 movement = new Vector2(x, y) * speed;
        rb.velocity = movement;

        //Rotate
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected void SelectVehicle()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        int index = Random.Range(0, 7);
        vehicle = (VehicleHandler.eVehicle)index;

        sr.sprite = VehicleHandler.VehicleToSprite(vehicle);
        speed = VehicleHandler.VehicleToSpeed(vehicle);
    }

    protected void SetFirstNode()
    {
        float shortDist = float.MaxValue;
        Node closestNode = null;
        Node[] nodes = FindObjectsOfType<Node>();
        foreach (Node node in nodes)
        {
            float dist = Vector2.Distance(transform.position, node.transform.position);
            if (dist < shortDist && dist != 0)
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
            else if (GetType() == typeof(RunningAI))
            {
                GameController.Instance.AddTime(2);
                GameController.Instance.obstacleScore += 200;
            }
            else
            {
                GameController.Instance.obstacleScore += 100;
            }
            Destroy(this.gameObject);
        }
    }
}
