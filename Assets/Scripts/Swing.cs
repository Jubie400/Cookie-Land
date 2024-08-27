using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static Cookie;

public class Swing : NetworkBehaviour
{
    public bool Up;
    public bool Down;
    public bool Stop;

    [SyncVar(hook = nameof(SetBool))]
    public bool Entered;

    public float Acceleration;

    [SyncVar(hook = nameof(SetRotation))]
    public float Rotation;

    public GameObject MobileButton;
    public GameObject Cookie;

    public Transform CookiePlacer;


    // Update is called once per frame
    void Update()
    {
        if (Cookie != null && Entered)
        {
            if (Cookie.GetComponent<Controller>().MobileInput.activeInHierarchy)
            {
                if (!Stop)
                {
                    MobileButton.SetActive(true);
                }
            }

            Vector2 Input = Cookie.GetComponent<Controller>().CookieController.actions["Move"].ReadValue<Vector2>();

            if (Acceleration > 1f)
            {
                Acceleration = 1;
            }

            if (Input.y < -0.2f)
            {
                if (Acceleration < 1)
                {
                    Acceleration += 0.1f;
                }

                Stop = false;
                Up = true;
                Down = false;

                if (GetComponent<Rigidbody>().rotation.x < 0.25f)
                {
                    GetComponent<Rigidbody>().AddTorque(Acceleration, 0, 0, ForceMode.Acceleration);
                }
                else
                {
                    GetComponent<Rigidbody>().AddTorque(-Acceleration, 0, 0, ForceMode.Acceleration);
                }

                if (GetComponent<Rigidbody>().rotation.x > 0.25f)
                {
                    GetComponent<Rigidbody>().AddTorque(-Acceleration, 0, 0, ForceMode.Acceleration);
                }
            }

            if (Input.y > 0.2f)
            {
                if (Acceleration < 1f)
                {
                    Acceleration += 0.1f;
                }
                Stop = false;
                Up = false;
                Down = true;

                if (GetComponent<Rigidbody>().rotation.x > -0.25f)
                {
                    GetComponent<Rigidbody>().AddTorque(-Acceleration, 0, 0, ForceMode.Acceleration);
                }
                else
                {
                    GetComponent<Rigidbody>().AddTorque(Acceleration, 0, 0, ForceMode.Acceleration);
                }

                if (GetComponent<Rigidbody>().rotation.x < -0.25f)
                {
                    GetComponent<Rigidbody>().AddTorque(Acceleration, 0, 0, ForceMode.Acceleration);
                }
            }


            if (Input.y == 0)
            {
                if (Acceleration > 0.50f)
                {
                    Acceleration -= 0.0008f;
                }
                else if (Acceleration <= 0.50f)
                {
                    Stop = true;
                }
                if (GetComponent<Rigidbody>().rotation.x > 0)
                {
                    GetComponent<Rigidbody>().AddTorque(-Acceleration, 0, 0, ForceMode.Acceleration);
                }

                if (GetComponent<Rigidbody>().rotation.x < 0)
                {
                    GetComponent<Rigidbody>().AddTorque(Acceleration, 0, 0, ForceMode.Acceleration);
                }
            }

            if (Cookie.GetComponent<Controller>().CookieController.actions["Jump"].WasPressedThisFrame())
            {
                if (Stop)
                {
                    Entered = false;
                    MobileButton.SetActive(false);
                    Cookie.GetComponent<Controller>().ActionType = 0;
                    Cookie.GetComponent<Controller>().Moveable = false;
                    Cookie.GetComponent<Rigidbody>().isKinematic = false;
                    Cookie.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3.5f);
                    Cookie = null;
                }
            }
            else
            {
                Cookie.GetComponent<Controller>().ActionType = 2;
                Cookie.GetComponent<Controller>().Moveable = true;
                Cookie.GetComponent<Rigidbody>().isKinematic = true;
                Cookie.transform.position = CookiePlacer.position;
                Cookie.transform.eulerAngles = CookiePlacer.eulerAngles;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Entered)
        {
            Entered = true;
            Cookie = collision.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Stop)
        {
            Acceleration = 0;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void SetBool(bool oldBool, bool newBool)
    {
        Entered = newBool;
    }

    public void SetRotation(float oldFloat, float newFloat)
    {
        Rotation = newFloat;
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        if(Cookie == NetworkClient.localPlayer.gameObject)
        {
            Entered = false;
            Cookie = null;
        }
    }

}
