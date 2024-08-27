using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Transition;
    //change tet name

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == NetworkClient.localPlayer.transform)
        {
            Fade();
        }
    }

    public void Fade()
    {
        Instantiate(Transition);
        StartCoroutine(test());
    }

    // rmeove and add aniamtion event to fade animation
    IEnumerator test()
    {
        yield return new WaitForSeconds(2);

        NetworkClient.localPlayer.gameObject.transform.position = new Vector3(NetworkClient.localPlayer.gameObject.GetComponent<Controller>().LastPositiion.x, NetworkClient.localPlayer.gameObject.GetComponent<Controller>().LastPositiion.y + 1f, NetworkClient.localPlayer.gameObject.GetComponent<Controller>().LastPositiion.z);
    }
}
