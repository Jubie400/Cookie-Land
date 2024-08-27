using Mirror.Examples.Pong;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject ElevatorDoor;

    public bool Down;

    public bool Wait;
    public bool Stopped;
    bool Open;
    bool Open2;
    bool Open3;

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y <= 1 && !Down)
        {
            if (!Wait)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
        }
        else if (transform.localPosition.y > -10.9f)
        {
            Down = true;
            if (!Wait)
            {
                transform.Translate(Vector3.down * Time.deltaTime);
            }
        }
        else if (transform.localPosition.y <= -10.9f)
        {
            Down = false;
        }

        if(Wait && !Stopped)
        {
            StartCoroutine(Stop());
        }

        List<Transform> Doors = new List<Transform>();

        foreach (Transform Door in ElevatorDoor.transform)
        {
            if (Door.name != "ElevatorDoors")
            {
                Doors.Add(Door);
            }
        }

        if (!Down)
        {
            if (transform.localPosition.y < -10.7 && !Open)
            {
                Doors[0].GetComponent<Animator>().SetTrigger("Open");
                Wait = true;
                Open = true;
            }

            if (transform.localPosition.y > -10.7 && Open)
            {
                Doors[0].GetComponent<Animator>().SetTrigger("Close");
                Open = false;
            }

            if (transform.localPosition.y > -4f && transform.localPosition.y < -3.8f && !Open2)
            {
                    Doors[1].GetComponent<Animator>().SetTrigger("Open2");
                Wait = true;
                Open2 = true;
            }

            if(transform.localPosition.y > -3.6f && Open2)
            {
                    Doors[1].GetComponent<Animator>().SetTrigger("Close2");
                Open2 = false;
                
            }

            if (transform.localPosition.y >= 0.98f && !Open3)
            {
                    Doors[2].GetComponent<Animator>().SetTrigger("Open3");
                Wait = true;
                Open3 = true;
            }
        }

        if (Down)
        {
            if (transform.localPosition.y > -4f && transform.localPosition.y < -3.8f && !Open2)
            {
                Doors[1].GetComponent<Animator>().SetTrigger("Open2");
                Open2 = true;
                Wait = true;
            }

            if (transform.localPosition.y < -4f && Open2)
            {
                Doors[1].GetComponent<Animator>().SetTrigger("Close2");
                Open2 = false;
            }

            if (transform.localPosition.y < 0.98f && Open3)
            {
                Doors[2].GetComponent<Animator>().SetTrigger("Close3");
                Open3 = false;
            }

        }
    }

    IEnumerator Stop()
    {
        Stopped = true;
        yield return new WaitForSeconds(5);

        Wait = false;
        Stopped = false;
    }
}
