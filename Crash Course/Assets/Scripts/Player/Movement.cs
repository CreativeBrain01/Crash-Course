using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(VehicleHandler))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    KeyCode[] upKeys;
    [SerializeField]
    KeyCode[] downKeys;
    [SerializeField]
    KeyCode[] leftKeys;
    [SerializeField]
    KeyCode[] rightKeys;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Input.simulateMouseWithTouches = true;
    }

    private void Update()
    {
        float initialSpeed = 2f;

        #region Keyboard Controls
        float x = 0, y = 0;
        foreach (KeyCode kc in upKeys)
        {
            if (Input.GetKey(kc)) y += initialSpeed;
        }
        foreach (KeyCode kc in downKeys)
        {
            if (Input.GetKey(kc)) y -= initialSpeed;
        }
        foreach (KeyCode kc in rightKeys)
        {
            if (Input.GetKey(kc)) x += initialSpeed;
        }
        foreach (KeyCode kc in leftKeys)
        {
            if (Input.GetKey(kc)) x -= initialSpeed;
        }
        #endregion

        //Prevent use of both keyboard controls for extra speed
        x = Mathf.Clamp(x, -initialSpeed, initialSpeed); y = Mathf.Clamp(y, -initialSpeed, initialSpeed);

        if (x == 0 && y == 0)
        {
            #region Mouse Controls
            if (Input.GetMouseButton(0))
            {
                Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dir = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
                x = Mathf.Clamp(dir.x, -initialSpeed, initialSpeed);
                y = Mathf.Clamp(dir.y, -initialSpeed, initialSpeed);
            }
            #endregion
            /*#region Touch Controls
            if (x == 0 && y == 0)
            {
                if (Input.touchCount > 0)
                {
                    Vector2 finger = Input.GetTouch(0).position;
                    Vector2 dir = new Vector2(finger.x - transform.position.x, finger.y - transform.position.y);
                    x = Mathf.Clamp(dir.x, -initialSpeed, initialSpeed);
                    y = Mathf.Clamp(dir.y, -initialSpeed, initialSpeed);
                }
            }
            #endregion*/
        }

        //Actually move
        Vector2 movement = new Vector2(x, y) * GetComponent<VehicleHandler>().Speed;
        rb.velocity = movement;

        //Rotate character
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
