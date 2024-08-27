using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using Mirror;
using TMPro;
using Cinemachine;
using static Cookie;
using static AccountManager;
using static CookieColor;
using UnityEngine.UI;
using NUnit.Framework;

public class Controller : NetworkBehaviour
{
    public PlayerInput CookieController;

    public NetworkAnimator CookieAnimator;

    public GameObject CookieMesh;
    public GameObject CameraPlacer;
    public GameObject CookieCamera;
    public GameObject MobileInput;

    public TMP_Text NameText;

    public List<Transform> cameraTransforms = new List<Transform>();

    public Vector3 LastPositiion;

    public bool Started;
    public bool Moveable; // type 2 anim where you can still move (slides, other things, etc)
    public bool HatOn; // type 2 anim where you can still move (slides, other things, etc)

    [SyncVar(hook = nameof(ColorChanged))]
    public Color Color;

    [SyncVar(hook = nameof(NameChanged))] 
    public string CookieName;

    public int ActionType;

    // Actions (type)
    // 0 = regular
    // 1 = can move, idle anim different (dancing, and other actions)
    // 2 = no movement, idle anim different (unlocking things, etc)
    // 3 = no movement and only idle (transitions, etc)

    void Start()
    {
        if (!isLocalPlayer)
        {
            CookieCamera.SetActive(false);
        }

        if(isLocalPlayer)
        {
            cookie = transform;

            switch (Random.Range(1, 11).ToString())
            {
                case "0":
                    break;
                case "1":
                    Color = brown;
                    break;
                case "2":
                    Color = blue;
                    break;
                case "3":
                    Color = black;
                    break;
                case "4":
                    Color = red;
                    break;
                case "5":
                    Color = pink; // pink
                    break;
                case "6":
                    Color = purple; // purple
                    break;
                case "7":
                    Color = green;
                    break;
                case "8":
                    Color = orange;
                    break;
                case "9":
                    Color = yellow;
                    break;
                case "10":
                    Color = gray;
                    break;
            }

            if (GameObject.Find("CookieManager").GetComponent<CookieManager>().Mobile)
            {
                MobileInput.SetActive(true);
            }

        }

       
    }

    void Update()
    {
      //  gameObject.name = CookieName;

        if (isLocalPlayer)
        {
            NameText.text = CookieName;

            if (HatOn)
            {
                NameText.rectTransform.localPosition = new Vector3(0, 2.1f, 0);
            }

            Vector2 Movement = CookieController.actions["Move"].ReadValue<Vector2>();

            Transform[] Buttons = MobileInput.GetComponentsInChildren<Transform>();

            Move(Movement, CookieController, Buttons, Moveable, ActionType);

            Animate(Movement, CookieController, CookieAnimator, ActionType);

            if (Hop != 0 && Grounded)
            {
                GetComponent<Rigidbody>().AddForce(0, 3, 0, ForceMode.Impulse);
                Grounded = false;
            }

            if (!Mouse.current.rightButton.isPressed)
            {
                CookieCamera.GetComponentInChildren<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 0;
                CookieCamera.GetComponentInChildren<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 0;
            }
            else
            {
                CookieCamera.GetComponentInChildren<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 100;
                CookieCamera.GetComponentInChildren<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 1;
            }
        }

        if (GameObject.Find("CookieCamera") != null)
        {
            Transform Camera = GameObject.Find("CookieCamera").transform;
            if (!cameraTransforms.Contains(Camera))
            {
                cameraTransforms.Add(Camera);
            }
        }

        foreach (Transform cameraTransform in cameraTransforms)
        {
            if (cameraTransform != null) // Check if the camera transform exists
            {
                Vector3 cameraDirection = (NameText.transform.position - cameraTransform.position).normalized;
                NameText.transform.forward = cameraDirection;
            }
        }

        Transform[] CookieParts = GetComponentsInChildren<Transform>();

        SetColor(CookieParts, Color);

    }

    private void NameChanged(string OldName, string NewName)
    {
        NameText.text = NewName;
    }

    private void ColorChanged(Color OldColor, Color NewColor)
    {
        Color = NewColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        JumpCount = 0;
        Grounded = true;
        if (collision.gameObject.layer != 4)
        {
            LastPositiion = collision.GetContact(0).point;

            if (CookieCamera.GetComponentInChildren<CinemachineFreeLook>().Follow == null && CookieCamera.GetComponentInChildren<CinemachineFreeLook>().LookAt == null)
            {
                CookieCamera.GetComponentInChildren<CinemachineFreeLook>().Follow = transform;
                CookieCamera.GetComponentInChildren<CinemachineFreeLook>().LookAt = transform;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 4) 
        {
            CookieCamera.GetComponentInChildren<CinemachineFreeLook>().Follow = null;
            CookieCamera.GetComponentInChildren<CinemachineFreeLook>().LookAt = null;
            ActionType = 3;
        }
    }

    [Command]
    public void CmdHat()
    {
        GameObject hatPrefab = GameObject.Find("CookieManager").GetComponent<NetworkManager>().spawnPrefabs[0];
        GameObject Hat = Instantiate(hatPrefab);
        NetworkServer.Spawn(Hat, connectionToClient);
        RpcHat(Hat);
    }

    [ClientRpc]
    void RpcHat(GameObject Hat)
    {
        Hat.GetComponent<TestHat>().Owner = gameObject.name;
    }

    [Command]
    void CmdUnspawnHat(GameObject Hat)
    {
        NetworkServer.UnSpawn(Hat);
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        if (transform.GetChild(0).transform.Find("TopHat(Clone)") != null)
        {
            GameObject test = transform.GetChild(0).transform.Find("TopHat(Clone)").gameObject;
            if (test != null)
            {
                CmdUnspawnHat(test);
            }
        }
    }
}
