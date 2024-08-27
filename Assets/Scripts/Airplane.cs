//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class Airplane : MonoBehaviour
//{
//    //public bool Moving;
//    public bool Flying;
//    public bool Crashing;
//    public bool Closed;

//    public float Speed;
//    public float EngineSpeed;
//    public float MaxEngineSpeed;
//    public float RotateSpeed;
//    public float Rotation;

//    public Transform Mesh;
//    public Transform Engine;

//    private Vector2 StartPosition;

//    void Update()
//    {
//        if(Keyboard.current.eKey.wasPressedThisFrame && Speed == 0) 
//        {
//            MaxEngineSpeed = 100;
//        }

//        Engine.Rotate(0, 0, EngineSpeed);

//        transform.Translate(0, 0, Speed * Time.deltaTime);

//        if (MaxEngineSpeed == 100 && EngineSpeed < 100)
//        {
//            EngineSpeed += 1f;
//        }

//        if (EngineSpeed > 0 && MaxEngineSpeed == 0)
//        {
//            EngineSpeed -= 1f;
//        }

//        if (EngineSpeed < 0)
//        {
//            EngineSpeed = 0;
//        }

//        if (Movement.y > 0)
//        {
//            if (Speed < 100 && EngineSpeed > 50 && !Crashing)
//            {
//                Speed += 0.1f;
//            }
//        }

//        if(Movement.y == 0) 
//        {
//            if (Speed > 10)
//            {
//                Speed -= 0.1f;
//            }
//            else
//            {
//                if (Flying)
//                {
//                    if (Closed)
//                    {
//                        Animator[] Wheels = GetComponentsInChildren<Animator>();

//                        foreach (Animator Wheel in Wheels)
//                        {
//                            Wheel.SetTrigger("Open");
//                        }

//                        Closed = false;
//                    }

//                    Debug.Log("PREPARE FOR IMPACT! - funny cookie man");
//                    GetComponent<Rigidbody>().isKinematic = false;
//                    Crashing = true;
//                }
//            }
//        }


//        if (Movement.x == 0 && RotateSpeed > 0)
//        {
//            RotateSpeed = 0;
//        }

//        if (Movement.x != 0)
//        {
//            if (RotateSpeed < 50)
//            {
//                RotateSpeed += 1f;
//            }
//        }
//        else
//        {
//            if (Flying)
//            {
//                Mesh.eulerAngles = new Vector3(Mesh.eulerAngles.x, Mesh.eulerAngles.y, Mathf.LerpAngle(Mesh.transform.eulerAngles.z, 0, Time.deltaTime));
//            }
//        }

//        if (Movement.x > 0)
//        {
//            transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);

//            if (Flying)
//            {
//                Mesh.eulerAngles = new Vector3(Mesh.eulerAngles.x, Mesh.eulerAngles.y, Mathf.LerpAngle(Mesh.transform.eulerAngles.z, -25, Time.deltaTime));
//            }

//        }
//        else if (Movement.x < 0)
//        {
//            transform.Rotate(0, -RotateSpeed * Time.deltaTime, 0);

//            if (Flying)
//            {
//                Mesh.eulerAngles = new Vector3(Mesh.eulerAngles.x, Mesh.eulerAngles.y, Mathf.LerpAngle(Mesh.transform.eulerAngles.z, 25, Time.deltaTime));
//            }
//        }

//        if (Mouse.current.leftButton.wasPressedThisFrame)
//        {
//            StartPosition = Mouse.current.position.ReadValue();

//            GetComponent<Rigidbody>().isKinematic = true;
//        }

//        if (Mouse.current.leftButton.isPressed && Speed > 0)
//        {
//            if (!Flying)
//            {
//                Flying = true;
//            }

//            Vector2 CurrentPosition = Mouse.current.position.ReadValue();
//            Vector2 Drag = CurrentPosition - StartPosition;

//            if (!Crashing)
//            {
//                Rotation = Mathf.Clamp(Drag.y, -25, 25);
//            }
//            else
//            {
//                Rotation = 0;
//            }

//            if (transform.position.y < 100)
//            {
//                transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, Rotation, Time.deltaTime), transform.eulerAngles.y, transform.eulerAngles.z);
//            }
//            else if (transform.position.y >= 100 && Rotation > 0)
//            {
//                transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, Rotation, Time.deltaTime), transform.eulerAngles.y, transform.eulerAngles.z);
//            }
//            else if (transform.position.y >= 100 && Rotation < 0)
//            {
//                transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, 0, Time.deltaTime), transform.eulerAngles.y, transform.eulerAngles.z);
//            }

//            if (Rotation < 0)
//            {
//                Animator[] Wheels = GetComponentsInChildren<Animator>();

//                if (!Closed)
//                {
//                    foreach (Animator Wheel in Wheels)
//                    {
//                        Wheel.SetTrigger("Close");
//                    }

//                    Closed = true;
//                }
//            }

//            if (Rotation > 0)
//            {
//                if (transform.position.y <= 50)
//                {
//                    if (Closed)
//                    {
//                        Animator[] Wheels = GetComponentsInChildren<Animator>();

//                        foreach (Animator Wheel in Wheels)
//                        {
//                            Wheel.SetTrigger("Open");
//                        }

//                        Closed = false;
//                    }
//                    Debug.Log("PREPARE FOR IMPACT! - funny cookie man");
//                    GetComponent<Rigidbody>().isKinematic = false;
//                    Crashing = true;
//                }
//                else
//                {
//                    GetComponent<Rigidbody>().isKinematic = true;
//                }
//            }
//        }

//        if (transform.position.y > 50)
//        {
//            transform.position = new Vector3(transform.position.x, 50, transform.position.z);
//        }

//        if (!Mouse.current.leftButton.isPressed)
//        {
//            transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, 0, Time.deltaTime), transform.eulerAngles.y, transform.eulerAngles.z);
//        }
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        if(Flying) 
//        {
//            Crashing = false;
//            MaxEngineSpeed = 0;
//            Speed = 0;
//            RotateSpeed = 0;
//            Flying = false;
//        }
//    }
//}
