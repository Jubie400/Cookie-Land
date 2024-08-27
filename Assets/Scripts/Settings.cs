using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle NameToggle;
    Transform[] CookieChildren;

    private void Start()
    {
        if (gameObject.name != "SettingBackground")
        {
            CookieChildren = NetworkClient.localPlayer.gameObject.GetComponentsInChildren<Transform>();
        }

        if (gameObject.name != "HatButton")
        {
            if (PlayerPrefs.HasKey("Nametag") == false)
            {
                PlayerPrefs.SetInt("Nametag", 1);
            }
            else
            {
                switch (PlayerPrefs.GetInt("Nametag"))
                {
                    case 0:
                        if (NetworkClient.localPlayer.gameObject.GetComponent<Controller>().NameText.gameObject.activeInHierarchy)
                        {
                            NetworkClient.localPlayer.gameObject.GetComponent<Controller>().NameText.gameObject.SetActive(false);
                        }
                        if (NameToggle != null)
                        {
                            NameToggle.isOn = false;
                        }
                        break;
                    case 1:
                        if (!NetworkClient.localPlayer.gameObject.GetComponent<Controller>().NameText.gameObject.activeInHierarchy)
                        {
                            NetworkClient.localPlayer.gameObject.GetComponent<Controller>().NameText.gameObject.SetActive(true);
                        }
                        if (NameToggle != null)
                        {
                            NameToggle.isOn = true;
                        }
                        break;
                }
            }
        }
        else
        {
            if (PlayerPrefs.HasKey("HatColor") == false)
            {
                PlayerPrefs.SetInt("HatColor", 0);
            }
            else
            {
                switch(PlayerPrefs.GetInt("HatColor"))
                {
                    case 0:
                        foreach (Transform Child in CookieChildren)
                        {
                            if (Child.name.Contains("TopHat"))
                            {
                                Child.GetComponentInChildren<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.black);
                                Child.GetComponentInChildren<MeshRenderer>().materials[1].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[1].SetColor("_EmissionColor", Color.white);
                                Child.GetComponentInChildren<MeshRenderer>().materials[2].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[2].SetColor("_EmissionColor", Color.white);
                            }
                        }
                        break;
                    case 1:
                        foreach (Transform Child in CookieChildren)
                        {
                            if (Child.name.Contains("TopHat"))
                            {
                                Child.GetComponentInChildren<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                                Child.GetComponentInChildren<MeshRenderer>().materials[1].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[1].SetColor("_EmissionColor", Color.black);
                                Child.GetComponentInChildren<MeshRenderer>().materials[2].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[2].SetColor("_EmissionColor", Color.black);
                            }
                        }
                        break;
                    case 2:

                        foreach (Transform Child in CookieChildren)
                        {
                            if (Child.name.Contains("TopHat"))
                            {
                                Child.GetComponentInChildren<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.blue);
                                Child.GetComponentInChildren<MeshRenderer>().materials[1].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[1].SetColor("_EmissionColor", Color.white);
                                Child.GetComponentInChildren<MeshRenderer>().materials[2].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[2].SetColor("_EmissionColor", Color.black);
                            }
                        }
                        break;
                    case 3:
                        foreach (Transform Child in CookieChildren)
                        {
                            if (Child.name.Contains("TopHat"))
                            {
                                Child.GetComponentInChildren<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_EmissionColor", CookieColor.purple);
                                Child.GetComponentInChildren<MeshRenderer>().materials[1].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[1].SetColor("_EmissionColor", Color.white);
                                Child.GetComponentInChildren<MeshRenderer>().materials[2].EnableKeyword("_EMISSION");
                                Child.GetComponentInChildren<MeshRenderer>().materials[2].SetColor("_EmissionColor", Color.black);
                            }
                        }
                        break;
                }
            }
        }

        PlayerPrefs.Save();
    }

    public void Save()
    {
        if (NameToggle != null)
        {
            if (EventSystem.current.currentSelectedGameObject == NameToggle.gameObject)
            {
                switch (NameToggle.isOn)
                {
                    case false:
                        PlayerPrefs.SetInt("Nametag", 0);
                        PlayerPrefs.Save();
                        NetworkClient.localPlayer.gameObject.GetComponent<Controller>().NameText.gameObject.SetActive(false);
                        break;
                    case true:
                        PlayerPrefs.SetInt("Nametag", 1);
                        PlayerPrefs.Save();
                        NetworkClient.localPlayer.gameObject.GetComponent<Controller>().NameText.gameObject.SetActive(true);
                        break;
                }
            }
        }

        if (gameObject.name == "HatBackground")
        {
            switch (EventSystem.current.currentSelectedGameObject.name)
            {
                case "Black":
                    PlayerPrefs.SetInt("HatColor", 0);
                    PlayerPrefs.Save();
                    foreach(Transform Child in CookieChildren)
                    {
                        if(Child.name.Contains("TopHat"))
                        {
                            Child.GetComponentInChildren<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.black);
                            Child.GetComponentInChildren<MeshRenderer>().materials[1].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[1].SetColor("_EmissionColor", Color.white);
                            Child.GetComponentInChildren<MeshRenderer>().materials[2].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[2].SetColor("_EmissionColor", Color.white);
                        }
                    }
                    break;
                case "White":
                    PlayerPrefs.SetInt("HatColor", 1);
                    PlayerPrefs.Save();
                    foreach (Transform Child in CookieChildren)
                    {
                        if (Child.name.Contains("TopHat"))
                        {
                            Child.GetComponentInChildren<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                            Child.GetComponentInChildren<MeshRenderer>().materials[1].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[1].SetColor("_EmissionColor", Color.black);
                            Child.GetComponentInChildren<MeshRenderer>().materials[2].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[2].SetColor("_EmissionColor", Color.black);
                        }
                    }
                    break;
                case "Blue":
                    PlayerPrefs.SetInt("HatColor", 2);
                    PlayerPrefs.Save();
                    foreach (Transform Child in CookieChildren)
                    {
                        if (Child.name.Contains("TopHat"))
                        {
                            Child.GetComponentInChildren<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.blue);
                            Child.GetComponentInChildren<MeshRenderer>().materials[1].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[1].SetColor("_EmissionColor", Color.white);
                            Child.GetComponentInChildren<MeshRenderer>().materials[2].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[2].SetColor("_EmissionColor", Color.black);
                        }
                    }
                    break;
                case "Purple":
                    PlayerPrefs.SetInt("HatColor", 3);
                    PlayerPrefs.Save();
                    foreach (Transform Child in CookieChildren)
                    {
                        if (Child.name.Contains("TopHat"))
                        {
                            Child.GetComponentInChildren<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_EmissionColor", CookieColor.purple);
                            Child.GetComponentInChildren<MeshRenderer>().materials[1].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[1].SetColor("_EmissionColor", Color.white);
                            Child.GetComponentInChildren<MeshRenderer>().materials[2].EnableKeyword("_EMISSION");
                            Child.GetComponentInChildren<MeshRenderer>().materials[2].SetColor("_EmissionColor", Color.black);
                        }
                    }
                    break;
            }
        }
    }

    public void Drag()
    {
        GetComponent<Image>().rectTransform.position = Mouse.current.position.ReadValue();
    }
}
