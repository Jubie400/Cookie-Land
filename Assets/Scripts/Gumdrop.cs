using Mirror;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gumdrop : MonoBehaviour
{
    public int Count;
    public int Total;
    public int CounterTotal;

    public bool Colliding;

    public GameObject DropPrefab;
    public GameObject Counter;
    public GameObject XP;

    private void Start()
    {
        if(gameObject.name.Contains("Gumdrop"))
        {
            Counter = GameObject.Find("Counter");
            XP = GameObject.Find("XPSlider");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "Counter")
        {
            GetComponentInChildren<TMP_Text>().text = CounterTotal.ToString();
        }

        if (gameObject.name != "Counter" && !gameObject.name.Contains("Gumdrop"))
        {
            Vector3 center = GetComponent<Collider>().bounds.center;
            Vector3 size = GetComponent<Collider>().bounds.size;

            while (Count < Total)
            {
                Vector3 randomPostion = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -center.y, Random.Range(-size.z / 2, size.z / 2));
                float GumDropColor = Random.value;
                Color DropColor = Color.black;

                if(GumDropColor >= 0.6f)
                {
                    DropColor = Color.green;
                }

                if(GumDropColor >= 0.3f && GumDropColor < 0.6f)
                {
                    DropColor = Color.blue;
                }

                if(GumDropColor >= 0 && GumDropColor < 0.3f)
                {
                    DropColor = Color.red;
                }

                GameObject GumdropObject = Instantiate(DropPrefab, randomPostion, Quaternion.identity);
                GumdropObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().materials[0].SetColor("_EmissionColor", DropColor);
                GumdropObject.transform.SetParent(transform);

                foreach(Transform Drop in transform)
                {
                    if(Vector3.Distance(randomPostion, Drop.position) < 5 || Drop.GetComponent<Gumdrop>().Colliding)
                    {
                        randomPostion = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -center.y, Random.Range(-size.z / 2, size.z / 2));
                        Drop.position = randomPostion;
                    }
                }

                Count += 1;
            }

            if (Count == Total)
            {
                foreach (Transform Drop in transform)
                {
                    if (Drop.GetComponent<Gumdrop>().Colliding)
                    {
                        Vector3 randomPostion = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -center.y, Random.Range(-size.z / 2, size.z / 2));
                            Drop.position = randomPostion;
                            Drop.GetComponent<Gumdrop>().Colliding = false;
                    }
                }
            }
        }

        if(gameObject.name.Contains("Gumdrop"))
        {
            transform.Rotate(0, -0.5f, 0);

            RaycastHit[] hit = Physics.SphereCastAll(transform.position, 2, Vector3.one, 2);

            if(hit != null)
            {
                foreach(RaycastHit point in hit) 
                {
                    if (!point.collider.name.Contains("Ground") && point.collider.name != "CookieMesh")
                    {
                        if (!point.collider.name.Contains("Gumdrop"))
                        {
                            Colliding = true;
                        }
                        else
                        {
                            if(point.collider.name != gameObject.name && Vector3.Distance(point.collider.gameObject.transform.position, gameObject.transform.position) < 5)
                            {
                                Colliding = true;
                            }
                        }
                    }
                }
            }

            RaycastHit GroundHit;

            if (Physics.Raycast(transform.position, Vector3.down, out GroundHit, Mathf.Infinity))
            {
                if(!GroundHit.collider.name.Contains("Ground"))
                {
                    Colliding = true;
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent == NetworkClient.localPlayer.transform)
        {
            GetComponent<Collider>().enabled = false;
            if (transform.GetChild(0).GetChild(0).GetComponent<Renderer>().materials[0].GetColor("_EmissionColor") == Color.green)
            {
                Counter.GetComponent<Gumdrop>().CounterTotal += 5;
                XP.GetComponent<XPSystem>().Total += Random.Range(1, 6);
            }

            if (transform.GetChild(0).GetChild(0).GetComponent<Renderer>().materials[0].GetColor("_EmissionColor") == Color.blue)
            {
                Counter.GetComponent<Gumdrop>().CounterTotal += 10;
                XP.GetComponent<XPSystem>().Total += Random.Range(6, 11);
            }

            if (transform.GetChild(0).GetChild(0).GetComponent<Renderer>().materials[0].GetColor("_EmissionColor") == Color.red)
            {
                Counter.GetComponent<Gumdrop>().CounterTotal += 15;
                XP.GetComponent<XPSystem>().Total += Random.Range(11, 16);
            }
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        GetComponentInChildren<Animator>().SetTrigger("Despawn");
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        transform.parent.GetComponent<Gumdrop>().Count -= 1;
    }
}
