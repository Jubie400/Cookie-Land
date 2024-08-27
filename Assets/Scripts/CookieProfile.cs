using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using UnityEngine.Networking;

public class CookieProfile : NetworkBehaviour
{
    public GameObject Profile;
    public GameObject ProfileAbout;

    public TMP_Text CookieName;
    public TMP_Text CookieAboutName;
    public TMP_Text CookieID;

    string clickedCookieName = "";

    private static CookieProfile activeProfile; // Store the currently active profile

    private void OnMouseDown()
    {
        // Close other profiles if already active
        if (activeProfile != null && activeProfile != this)
        {
            activeProfile.CloseProfile();
        }

        Profile.GetComponent<RectTransform>().localPosition = Vector3.zero;
        if (gameObject != NetworkClient.localPlayer.gameObject)
        {
            GetName();
        }
    }

    private void GetName()
    {
        // Find all cookies in the scene
        CookieProfile[] cookies = FindObjectsByType<CookieProfile>(FindObjectsSortMode.None);

        // Find the clicked cookie and its name
        CookieProfile clickedCookie = null;
        foreach (CookieProfile cookie in cookies)
        {
            if (cookie.gameObject == gameObject)
            {
                clickedCookie = cookie;
                //clickedCookieName = cookie.GetComponent<Nametag>().NameText.text;

                break;
            }
        }

        Profile.SetActive(true);
        CookieName.text = clickedCookieName;

        // Set this profile as the active profile
        activeProfile = this;
    }

    public void About()
    {
        ProfileAbout.SetActive(true);
        CookieAboutName.text = CookieName.text;
    }

    private void CloseProfile()
    {
        // Close the profile and reset the active profile
        Profile.SetActive(false);
        activeProfile = null;
    }
}
