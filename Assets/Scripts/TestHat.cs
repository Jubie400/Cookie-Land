using Mirror;
using UnityEngine;

public class TestHat : NetworkBehaviour
{
    GameObject Cookie;
    GameObject Hat;

    [SyncVar(hook = nameof(SetName))]
    public string Owner;

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.name.Contains("TopHat"))
        {
            transform.Rotate(0, 2, 0);
        }
        else
        {
            if (Owner != "")
            {
                if (transform.parent == null)
                {
                    transform.parent = GameObject.Find(Owner).transform.GetChild(0).transform;
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NetworkClient.localPlayer.gameObject.GetComponent<Controller>().CmdHat();
        gameObject.SetActive(false);
    }

    void SetName(string oldName, string newName)
    {
        Owner = newName;
    }

}
