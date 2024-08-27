using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Currency : MonoBehaviour
{
    public GameObject Milk;
    public GameObject Straw;
    public TMP_Text Counter;
    public TMP_Text Name;
    int Points;
   public int Started;

    public bool GotPoints;

    private List<Transform> cameraTransforms = new List<Transform>();

    Color MilkColor;

    private void Start()
    {
        if(gameObject.name != "CurrencyTotal")
        {
            float test = Random.value;

            if(test <= 0.8f)
            {
                float test2 = Random.Range(0, 2);

                switch(test2)
                {
                    case 0:
                        transform.parent.name = "Milk";
                        MilkColor = Color.white;
                        Milk.GetComponent<Renderer>().material.SetColor("_EmissionColor", MilkColor);
                        Milk.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                        Straw.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.red);
                        Straw.GetComponent<Renderer>().materials[1].EnableKeyword("_EMISSION");
                        break;
                    case 1:
                        transform.parent.name = "ChocolateMilk";
                        MilkColor = new Color(0.3277781f, 0.05126947f, 0f);
                        Milk.GetComponent<Renderer>().material.SetColor("_EmissionColor", MilkColor);
                        Milk.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                        Straw.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", MilkColor);
                        Straw.GetComponent<Renderer>().materials[1].EnableKeyword("_EMISSION");
                        break;
                }
                
            }
            else
            {
                transform.parent.name = "GoldenMilk";
                MilkColor = new Color(1f, 0.4377883f, 0.02352941f);
                Milk.GetComponent<Renderer>().material.SetColor("_EmissionColor", MilkColor);
                Milk.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                Straw.GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", MilkColor);
                Straw.GetComponent<Renderer>().materials[1].EnableKeyword("_EMISSION");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "CurrencyTotal")
        {
            if (Started == 0)
            {
                StartCoroutine(GetPoints());
                Started += 1;
            }
        }
        else
        {
            transform.parent.Rotate(0, 0.75f, 0);
            if (NetworkClient.localPlayer != null)
            {
                Counter = GameObject.Find("MilkPoints").GetComponent<TMP_Text>();
            }

            if(transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MilkClaim") && transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.gameObject == NetworkClient.localPlayer.gameObject && other.gameObject.tag != "CookieCamera" && GameObject.Find("CurrencyTotal").GetComponent<Currency>().GotPoints)
        {
            transform.parent.GetComponent<Animator>().SetTrigger("Claim");
            Counter = GameObject.Find("CurrencyTotal").GetComponent<Currency>().Counter;
            Name = GameObject.Find("CurrencyTotal").GetComponent<Currency>().Name;
            int.TryParse(Counter.text, out Points);
            if (transform.parent.name == "Milk" && Points < 1000000)
            {
                Points += 10;
                Counter.text = Points.ToString();
            }

            if (transform.parent.name == "ChocolateMilk" && Points < 1000000)
            {
                Points += 50;
                Counter.text = Points.ToString();
            }

            if (transform.parent.name == "GoldenMilk" && Points < 1000000)
            {
                Points += 100;
                Counter.text = Points.ToString();
            }

            if(Points > 1000000)
            {
                Points = 1000000;
                Counter.text = Points.ToString();
            }

            StartCoroutine(AddPoints());
        }
    }

    IEnumerator AddPoints() 
    {
        Debug.Log("adding");

        WWWForm form = new WWWForm();
        form.AddField("username", Name.text);
        form.AddField("points", Points);
        form.AddField("setting", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://cookiezbase.000webhostapp.com/points.php", form))
        {
            yield return www.SendWebRequest();

            if (www.downloadHandler.text == "0")
            {
                Debug.Log("milk added");
            }
        }
    }

    IEnumerator GetPoints() 
    {
        Debug.Log("Getting");

        WWWForm form = new WWWForm();
        form.AddField("username", Name.text);
        form.AddField("points", "");
        form.AddField("setting", 2);

        using (UnityWebRequest www = UnityWebRequest.Post("https://cookiezbase.000webhostapp.com/points.php", form))
        {
            yield return www.SendWebRequest();

            Counter.text = www.downloadHandler.text;
            GotPoints = true;
        }
    }
}
