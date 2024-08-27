using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using static Cookie;
using UnityEngine.InputSystem;

public class SmoreController : NetworkBehaviour
{

    public float rotationSpeed;   // Current rotation speed
    public float maxRotationSpeed; // Maximum rotation speed
    public float rotationAcceleration; // Acceleration of rotation speed
    public float rotationDeceleration; // Deceleration of rotation speed

    private void Start()
    {
        if (!isLocalPlayer) return;

        // Initialize rotation speed
        rotationSpeed = 0;
    }

    private void Update()
    {
        PlayerInput Cookie = cookie.GetComponent<Controller>().CookieController;
        Vector2 Movement = Cookie.actions["Move"].ReadValue<Vector2>();

        if (Movement.y != 0)
        {
            rotationSpeed = Mathf.Clamp(rotationSpeed + rotationAcceleration * Time.deltaTime, 0, maxRotationSpeed);
        }
        else
        {
            rotationSpeed = Mathf.Clamp(rotationSpeed - rotationDeceleration * Time.deltaTime, 0, maxRotationSpeed);
        }

        // Rotate the smore on the y-axis
        if (Movement.y > 0)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if(Movement.y < 0)
        {
            transform.Rotate(Vector3.down, rotationSpeed * Time.deltaTime);
        }
        else if(Movement.y == 0 && transform.eulerAngles.y != 180)
        {
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, 0, 1.2f * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, cookie.position, 1 * Time.deltaTime);
    }
}
