using Mirror;
using UnityEngine;

public class Slide : NetworkBehaviour
{
    public GameObject SlideTrigger;
    public GameObject StopTrigger;
    public GameObject SlidePlacer;
    public GameObject Disconnect;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "SlideTrigger")
        {
            if (other.gameObject.transform.parent == NetworkClient.localPlayer.transform)
            {
                NetworkClient.localPlayer.GetComponent<Controller>().ActionType = 2;
                NetworkClient.localPlayer.GetComponent<Controller>().Moveable = true;

                NetworkClient.localPlayer.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                if (!gameObject.transform.parent.name.Contains("2"))
                {
                    NetworkClient.localPlayer.transform.position = transform.position - new Vector3(-0.7f, 0, 0);
                }
                else
                {
                    NetworkClient.localPlayer.transform.position = transform.position - new Vector3(0.7f, 0, 0);
                }
            }
        }

        if (gameObject.name == "StopTrigger")
        {
            if (other.gameObject.transform.parent == NetworkClient.localPlayer.transform)
            {
                NetworkClient.localPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                NetworkClient.localPlayer.transform.eulerAngles = new Vector3(0, SlideTrigger.transform.eulerAngles.y, 0);
                NetworkClient.localPlayer.GetComponent<Controller>().ActionType = 0;
                NetworkClient.localPlayer.GetComponent<Controller>().Moveable = false; // only for type 2
                transform.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(gameObject.name == "SlideTrigger")
        {
            if (other.gameObject.transform.parent == NetworkClient.localPlayer.transform)
            {
                NetworkClient.localPlayer.GetComponent<Controller>().CookieAnimator.SetTrigger("Idle");
                NetworkClient.localPlayer.GetComponent<Rigidbody>().isKinematic = true;
                Vector2 Input = NetworkClient.localPlayer.GetComponent<Controller>().CookieController.actions["Move"].ReadValue<Vector2>();

                if (Input.y > 0)
                {
                    NetworkClient.localPlayer.transform.Translate(Vector3.up * Time.deltaTime);

                    if (other.bounds.center.y > 15.65f)
                    {
                        NetworkClient.localPlayer.transform.position = SlidePlacer.transform.position;
                        NetworkClient.localPlayer.GetComponent<Rigidbody>().isKinematic = false;
                        NetworkClient.localPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    }
                }

                if (Input.y < 0)
                {
                    NetworkClient.localPlayer.transform.Translate(Vector3.down * Time.deltaTime);

                    if (other.bounds.center.y < 12)
                    {
                        NetworkClient.localPlayer.transform.position = Disconnect.transform.position;
                        NetworkClient.localPlayer.GetComponent<Rigidbody>().isKinematic = false;
                        NetworkClient.localPlayer.GetComponent<Controller>().ActionType = 0;
                    }
                }
            }
        }
    }

    // i broke this but it dont matter anyway
}
