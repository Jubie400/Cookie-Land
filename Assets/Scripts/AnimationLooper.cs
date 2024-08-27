using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cookie;

public class AnimationLooper : MonoBehaviour
{
    NetworkAnimator CookieAnimator;

    public int HopCount;

    private void Start()
    {
        CookieAnimator = NetworkClient.localPlayer.GetComponent<NetworkAnimator>();
    }

    public void HopLoop()
    {

        if (Hop == 1 && CookieAnimator.animator.GetCurrentAnimatorStateInfo(0).speed > 0 && !CookieAnimator.animator.IsInTransition(0))
        {
            CookieAnimator.SetTrigger("Hop");
        }

        if (Hop == -1 && CookieAnimator.animator.GetCurrentAnimatorStateInfo(0).speed < 0 && !CookieAnimator.animator.IsInTransition(0))
        {
            CookieAnimator.SetTrigger("ReverseHop");
        }
    }

    public void Dance(int Step)
    {
        switch (Step)
        {
            case 1:
                if (NetworkClient.localPlayer.gameObject.GetComponent<Controller>().CookieController.actions["Move"].ReadValue<Vector2>().y == 0 && JumpCount == 0)
                {
                    CookieAnimator.animator.Play("Hop");
                    NetworkClient.localPlayer.gameObject.GetComponent<Rigidbody>().AddForce(0, 3, 0, ForceMode.Impulse);
                    StartCoroutine(Wait(0.65f, "Hop", 3, "Dance"));
                }
                else
                {
                    Step = 0;
                }
                break;
            case 2:
                if (NetworkClient.localPlayer.gameObject.GetComponent<Controller>().CookieController.actions["Move"].ReadValue<Vector2>().y == 0 && JumpCount == 0)
                {
                    CookieAnimator.animator.Play("Dance", 0, 0.02f);
                }
                else
                {
                    Step = 0;
                }
                break;
        }
    }

    public void Dance2()
    {
        CookieAnimator.SetTrigger("Dance2");
    }   

    public void Dance3()
    {
        CookieAnimator.SetTrigger("Dance3");
    }

    IEnumerator Wait(float Time, string AnimationName, float Force, string VoidName)
    {
        if (cookie.GetComponent<Controller>().CookieController.actions["Move"].ReadValue<Vector2>().y == 0 && JumpCount == 0)
        {
            yield return new WaitForSeconds(Time);

            if (Grounded)
            {
                CookieAnimator.animator.Play(AnimationName, 0, 0);
                NetworkClient.localPlayer.gameObject.GetComponent<Rigidbody>().AddForce(0, Force, 0, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(Time);

            switch (VoidName)
            {
                case "Dance":
                    Dance(2);
                    break;
            }
        }
    }

}
