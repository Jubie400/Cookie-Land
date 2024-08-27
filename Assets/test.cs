using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
    public GameObject testt;
    //Vector2 StartPosition;
    //Vector2 CurrentPosition;

    //private void Start()
    //{
    //    StartPosition = Mouse.current.position.ReadValue();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    CurrentPosition = Mouse.current.position.ReadValue();
    //    Vector2 Drag = CurrentPosition - StartPosition;
    //    transform.localPosition = new Vector3(-Mouse.current.position.ReadValue().x / 100000 * 10, 0, 0);

    //    //if(transform.position.x > 1)
    //    //{
    //    //    transform.position = new Vector3(1, transform.position.y, transform.position.z);
    //    //}
    //}

    private void Update()
    {
            Debug.Log("aaa");
            NetworkClient.RegisterPrefab(testt);
    }
}
