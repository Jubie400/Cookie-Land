using Mirror;
using UnityEngine;
using static CookieColor;

public class Ball : MonoBehaviour
{
    public int BallCount;
    public int Balls;
    bool Counting;

    public GameObject BallPrefab;

    private void Update()
    {
            if (Balls < BallCount)
            {
                if (!Counting)
                {
                    InvokeRepeating("Wait", 0f, 0.15f);
                    Counting = true;
                }
            }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PhysicsMaterial BallBounce = new PhysicsMaterial();
            BallBounce.bounciness = 1;
            GetComponent<SphereCollider>().material = BallBounce;

           GetComponent<Rigidbody>().AddForce(new Vector3(0, 0.01f, 0), ForceMode.Impulse);
        }
    }

    void Wait()
    {
        GameObject Ball = Instantiate(BallPrefab, transform);

        int BallColor = Random.Range(0, 5);

        switch (BallColor)
        {
            case 0:
                Ball.GetComponent<Renderer>().material.SetColor("_EmissionColor", blue);
                break;
            case 1:
                Ball.GetComponent<Renderer>().material.SetColor("_EmissionColor", red);
                break;
            case 2:
                Ball.GetComponent<Renderer>().material.SetColor("_EmissionColor", green);
                break;
            case 3:
                Ball.GetComponent<Renderer>().material.SetColor("_EmissionColor", pink);
                break;
            case 4:
                Ball.GetComponent<Renderer>().material.SetColor("_EmissionColor", purple);
                break;
        }

        Balls += 1;

        if(Balls == BallCount)
        {
            CancelInvoke();
        }
    }
}
