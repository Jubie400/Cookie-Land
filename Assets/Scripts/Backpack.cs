using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Backpack : MonoBehaviour
{
    public int Count;
    public int Zoom;
    public Camera MapCamera;

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame && EventSystem.current.currentSelectedGameObject.name == "ZoomButton")
        {
            Count -= 1;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && EventSystem.current.currentSelectedGameObject.name == "ZoomButton2")
        {
            Count += 1;
        }

        switch (Count)
        {
            case < 0:
                Count = 0;
                break;
            case 0:
                Zoom = 10;
                break;
            case 1:
                Zoom = 15;
                break;
            case 2:
                Zoom = 20;
                break;
            case 3:
                Zoom = 25;
                break;
            case 4:
                Zoom = 30;
                break;
            case > 4:
                Count = 4;
                break;
        }

        MapCamera.transform.position = new Vector3(NetworkClient.localPlayer.gameObject.transform.position.x, NetworkClient.localPlayer.gameObject.transform.position.y + Zoom, NetworkClient.localPlayer.gameObject.transform.position.z);
    }
}
