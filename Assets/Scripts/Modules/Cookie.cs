using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public static class Cookie
{
    public static Transform cookie;

    public static float Speed;
    public static float RotateSpeed;

    public static int Idle;
    public static int Hop;
    public static int JumpCount;

    public static bool Grounded;

    public static void SetColor(Transform[] CookieParts, Color CookieColor)
    {
        foreach (Transform CookiePart in CookieParts)
        {
            if (CookiePart.name.Contains("Eyelid"))
            {
                CookiePart.GetComponent<Renderer>().material.SetColor("_EmissionColor", CookieColor / 2);
                CookiePart.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            }

            if (CookiePart.name == "Body")
            {
                CookiePart.GetComponent<Renderer>().material.SetColor("_EmissionColor", CookieColor);
                CookiePart.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            }

            if (CookieColor != Color.black)
            {
                if (CookiePart.name.Contains("Chip"))
                {
                    CookiePart.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
                    CookiePart.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                }

                if (CookiePart.name == ("Mouth"))
                {
                    CookiePart.GetComponent<Renderer>().material.SetColor("_EmissionColor", CookieColor / 3);
                    CookiePart.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                }   
            }

            if (CookieColor == Color.black)
            {
                if (CookiePart.name.Contains("Eyelid"))
                {
                    CookiePart.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
                    CookiePart.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                }

                if (CookiePart.name == "Body")
                {
                    CookiePart.GetComponent<Renderer>().materials[1].SetColor("_Color", Color.white);
                }

                if (CookiePart.name.Contains("Chip") || CookiePart.name.Contains("Mouth"))
                {
                    CookiePart.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
                    CookiePart.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                }

                if (CookiePart.name == ("Mouth"))
                {
                    CookiePart.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
                    CookiePart.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                }
            }
        }
    }

    public static void Move(Vector2 Input, PlayerInput Controller, Transform[] MobileButtons, bool Moveable, int Action)
    {
        if (!Controller.actions["Run"].IsInProgress())
        {
            Speed = 3f;
        }
        else
        {
            Speed = 6;
        }

        RotateSpeed = 150;

        if (Action <= 1)
        {
            cookie.Translate(0, 0, Input.y * Speed * Time.deltaTime);

            if (Input.x > 0.2f || Input.x < -0.2f)
            {
                cookie.Rotate(0, Input.x * RotateSpeed * Time.deltaTime, 0);
            }
        }

        switch (Action)
        {
            case 0:
                MobileButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                MobileButtons[3].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                MobileButtons[4].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            case 1:
                MobileButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                MobileButtons[3].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                MobileButtons[4].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                break;
            case 2:
                if (!Moveable)
                {
                    MobileButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                    MobileButtons[3].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                }
                else
                {
                    MobileButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    MobileButtons[3].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }

                MobileButtons[4].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                break;
            case 3:
                MobileButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                MobileButtons[3].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                MobileButtons[4].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                break;
        }
    }

    public static void Animate(Vector2 Input, PlayerInput Controller, NetworkAnimator CookieAnimator, int Action)
    {
        if (Action <= 1)
        {

            Idle = 0;

            if (JumpCount == 0 && !CookieAnimator.animator.IsInTransition(0))
            {
                //GameObject.Find("HopStopper").GetComponent<BoxCollider>().enabled = true;
                if (Input.y > 0.2f)
                {
                    if (Hop != 1)
                    {
                        CookieAnimator.SetTrigger("Hop");
                        Hop = 1;
                    }
                }
                else if (Input.y < -0.2f)
                {
                    if (Hop != -1)
                    {
                        CookieAnimator.SetTrigger("ReverseHop");
                        Hop = -1;
                    }
                }
            }

            if (Controller.actions["Jump"].WasPressedThisFrame())
            {
                Hop = 0;
                if (JumpCount == 1 && cookie.GetComponent<Rigidbody>().linearVelocity != Vector3.zero)
                {
                    cookie.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                }

                if (JumpCount < 2)
                {
                    cookie.GetComponent<Rigidbody>().AddForce(0, 5, 0, ForceMode.Impulse);
                    JumpCount += 1;
                    CookieAnimator.SetTrigger("Jump");
                    Grounded = false;
                }
            }
              
        }
        else
        {
            Hop = 0;
        }


        if (Action == 0 || Action == 3)
        {
            if (Input.y == 0)
            {
                Hop = 0;
                if (JumpCount == 0 && Idle != 1 && !CookieAnimator.animator.IsInTransition(0))
                {
                    CookieAnimator.SetTrigger("Idle");
                    Idle = 1;
                }
            }
               
        }
        else
        {
            if (Input.y == 0)
            {
                Hop = 0;
                Idle = 0;
            }
        }

        ResetAnimation(CookieAnimator);
    }

    static void ResetAnimation(NetworkAnimator CookieAnimator)
    {
        if(Hop == 1)
        {
            CookieAnimator.ResetTrigger("ReverseHop");
            CookieAnimator.ResetTrigger("Idle");
            CookieAnimator.ResetTrigger("Jump");
            CookieAnimator.ResetTrigger("ReverseJump");
        }

        if(Hop == -1)
        {
            CookieAnimator.ResetTrigger("Hop");
            CookieAnimator.ResetTrigger("Idle");
            CookieAnimator.ResetTrigger("Jump");
            CookieAnimator.ResetTrigger("ReverseJump");
        }

        if(Idle == 1)
        {
            CookieAnimator.ResetTrigger("Hop");
            CookieAnimator.ResetTrigger("ReverseHop");
            CookieAnimator.ResetTrigger("Jump");
            CookieAnimator.ResetTrigger("ReverseJump");
        }

        if (JumpCount != 0 && CookieAnimator.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            CookieAnimator.ResetTrigger("ReverseHop");
            CookieAnimator.ResetTrigger("Hop");
            CookieAnimator.ResetTrigger("Idle");
            CookieAnimator.ResetTrigger("ReverseJump");
        }

        if (JumpCount != 0 && CookieAnimator.animator.GetCurrentAnimatorStateInfo(0).IsName("ReverseJump"))
        {
            CookieAnimator.ResetTrigger("Hop");
            CookieAnimator.ResetTrigger("ReverseHop");
            CookieAnimator.ResetTrigger("Idle");
            CookieAnimator.ResetTrigger("Jump");
        }
    }
}
