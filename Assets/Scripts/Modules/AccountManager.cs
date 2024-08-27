using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.Networking;

public class AccountManager: MonoBehaviour
{
    public TMP_InputField EmailField;
    public TMP_InputField NameField;
    public TMP_InputField PasswordField;
    public TMP_InputField ConfirmPasswordField;

    public string Name;

    public bool Vaild()
    {
        if (!EmailField.gameObject.activeInHierarchy && !ConfirmPasswordField.gameObject.activeInHierarchy) 
        {
            if(NameField.text.Length >= 3 && PasswordField.text.Length >= 6) 
            {
                return true;
            }
        }
        else
        {
            if(EmailField.text.Length > 1 && NameField.text.Length >= 3 && ConfirmPasswordField.text.Length >= 6)
            {
                return true;
            }
        }

        return false;
    }

    public void Send()
    {
        if(!EmailField.gameObject.activeInHierarchy && !ConfirmPasswordField.gameObject.activeInHierarchy)
        {
            if (Vaild())
            {
                StartCoroutine(Login());
            }
        }
        else
        {
            if (Vaild())
            {
                StartCoroutine(Register());
            }
        }
    }

    IEnumerator Register()
    {
        WWWForm RegisterForm = new WWWForm();
       // RegisterForm.AddField("Email", EmailField.text);
        RegisterForm.AddField("Name", NameField.text);
        RegisterForm.AddField("Password", ConfirmPasswordField.text);

        using(UnityWebRequest RegisterRequest = UnityWebRequest.Post("http://cookie-land.com/php/register.php", RegisterForm))
        {
            yield return RegisterRequest.SendWebRequest();

            if(RegisterRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("e");
            }
            else
            {
                Debug.Log(RegisterRequest.error);
            }

            Debug.Log(RegisterRequest.downloadHandler.text);
        }
    }

    IEnumerator Login()
    {
        WWWForm LoginForm = new WWWForm();
        LoginForm.AddField("Name", NameField.text);
        LoginForm.AddField("Password", ConfirmPasswordField.text);

        using (UnityWebRequest LoginRequest = UnityWebRequest.Post("", LoginForm))
        {
            yield return LoginRequest;

            if (LoginRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("e");
            }
            else
            {
                Debug.Log(LoginRequest.error);
            }
        }
    }

    public bool VaildInput()
    {
        if(EmailField != null && ConfirmPasswordField != null)
        {
            if (EmailField.text.Contains("@") && EmailField.text.Contains(".") && NameField.text.Length >= 4 && PasswordField.text.Length >= 6 && ConfirmPasswordField.text.Equals(PasswordField.text))
            {
                return true;
            }
        }
        else
        {
            if (NameField.text.Length >= 4 && PasswordField.text.Length >= 6)
            {
                return true;
            }
        }

        return false;
    }
}
