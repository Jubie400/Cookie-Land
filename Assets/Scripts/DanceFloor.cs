using Mirror;
using System.Collections;
using UnityEngine;
using static CookieColor;
using static Cookie;

public class DanceFloor : NetworkBehaviour
{
    [SyncVar]
    int LightColor;

    [SyncVar]
    bool LightChange;

    [SyncVar]
    bool Started;

    // Update is called once per frame
    void Update()
    {
        if (!LightChange)
        {
            StartCoroutine(Change());
            LightChange = true;
        }

        switch (LightColor)
        {
            case 1:
                GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", blue);
                break;
            case 2:
                GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", red);
                break;
            case 3:
                GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", green);
                break;
            case 4:
                GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", purple);
                break;
        }
    }

    IEnumerator Change()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            LightColor = Random.Range(1, 5);

            yield return new WaitForSeconds(0.1f);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.parent == NetworkClient.localPlayer.transform && NetworkClient.localPlayer != null)
        {
            NetworkClient.localPlayer.GetComponent<Controller>().ActionType = 1;

            Vector2 Movement = NetworkClient.localPlayer.GetComponent<Controller>().CookieController.actions["Move"].ReadValue<Vector2>();

            if (Movement.y == 0)
            {
                if (!Started)
                {
                    Started = true;
                    int Dance = 0;
                    if (!NetworkClient.localPlayer.gameObject.GetComponent<Controller>().HatOn)
                    {
                        Dance = Random.Range(1, 4);
                    }
                    else
                    {
                        Dance = 2;
                    }

                    switch (Dance)
                    {
                        case 1:
                                NetworkClient.localPlayer.GetComponent<Controller>().CookieAnimator.SetTrigger("Dance");
                            break;
                        case 2:
                            NetworkClient.localPlayer.GetComponent<Controller>().CookieAnimator.SetTrigger("Dance2");
                            break;
                        case 3:
                            NetworkClient.localPlayer.GetComponent<Controller>().CookieAnimator.SetTrigger("Dance3");
                            break;
                    }
                }
            }
            else
            {
                Started = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent == NetworkClient.localPlayer.transform && NetworkClient.localPlayer.transform != null)
        {
            NetworkClient.localPlayer.GetComponent<Controller>().NameText.transform.SetParent(NetworkClient.localPlayer.GetComponent<Controller>().CookieMesh.transform);
            NetworkClient.localPlayer.GetComponent<Controller>().ActionType = 0;
        }
    }
}
