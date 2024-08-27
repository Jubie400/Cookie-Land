using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public int Fade;

    public bool Done;

    public GameObject Animation;

    private void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);

            Animation.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            GetComponentInChildren<Animator>().SetTrigger("Fade");
    }

    private void Update()
    {
        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            NetworkClient.localPlayer.gameObject.GetComponent<Controller>().ActionType = 0;
            Destroy(gameObject);
        }
    }
}
