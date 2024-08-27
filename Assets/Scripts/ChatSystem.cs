using Mirror;
using Mirror.SimpleWeb;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChatSystem : NetworkBehaviour
{
    // A method to send chat messages

    public TMP_InputField ChatField;

    private void Update()
    {
        if(NetworkClient.localPlayer == null)
        {
            gameObject.SetActive(false);
        }
    }

    public void Chat()
    {
        if (ChatField.text != "")
        {
            CmdChat();
        }
    }

    public void Select()
    {
        NetworkClient.localPlayer.gameObject.GetComponent<Controller>().ActionType = 3;
    }

    public void Deselect()
    {
        NetworkClient.localPlayer.gameObject.GetComponent<Controller>().ActionType = 0;
    }

    [Command(requiresAuthority = false)]
    void CmdChat()
    {
        GameObject Message = Instantiate(GameObject.Find("CookieManager").GetComponent<NetworkManager>().spawnPrefabs[1]);
        NetworkServer.Spawn(Message);
        Message.transform.SetParent(GameObject.Find("Content").transform);
        Message.GetComponent<TMP_Text>().text = NetworkClient.localPlayer.gameObject.GetComponent<Controller>().CookieName + ": " + ChatField.text;
        ChatField.text = "";
        NetworkClient.localPlayer.gameObject.GetComponent<Controller>().ActionType = 0;
    }
}
