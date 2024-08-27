using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
    public Animator CookieAnimator;

    public void Hover()
    {
        Debug.Log("tes");
        if (!CookieAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hover") || CookieAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hover") && CookieAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            CookieAnimator.SetTrigger("Hover");
            Debug.Log("Hover");
        }
    }
}
