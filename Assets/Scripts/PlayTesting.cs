using UnityEngine;
using Mirror;
using TMPro;

public class PlayTesting : MonoBehaviour
{
    Controller[] test;
    public TMP_InputField NameField;
    public GameObject Settings;
    public GameObject Chat;
    bool enter;
    public void Play()
    {
        if (NameField.text.Length >= 3)
        {
            if (test.Length > 0)
            {
                foreach (Controller controller in test)
                {
                    if (controller.NameText.text != NameField.text)
                    {
                        enter = true;
                    }
                    else
                    {
                        enter = false;
                    }
                }
            }
            else
            {
                enter = true;
            }

            if (enter)
            {
                Camera.main.gameObject.SetActive(false);
                NetworkClient.AddPlayer();
            }
        }
    }

    private void Update()
    {
        test = GameObject.FindObjectsByType<Controller>(FindObjectsSortMode.None);

        if (NetworkClient.localPlayer != null && NetworkClient.localPlayer.gameObject != null)
        {
            GameObject.Find("Menu2").SetActive(false);
            Chat.SetActive(true);
            Settings.SetActive(true);
        }
    }
}
