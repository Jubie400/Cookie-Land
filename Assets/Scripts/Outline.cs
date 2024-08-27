using System.Collections.Generic;
using UnityEngine;
using static Cookie;

public class Outline : MonoBehaviour
{
    public Material OutlineMat;
    public float Scale;

    public int Count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Renderer[] tet = GetComponentsInChildren<Renderer>();
        foreach(Renderer tets in tet)
        {
            if(transform == cookie && tets.gameObject.name == "Body")
            {
                List<Material> BodyMats = new List<Material>();

                foreach(Material material in tets.materials) 
                {
                    if(!BodyMats.Contains(material))
                    {
                        BodyMats.Add(material);
                    }
                }

                BodyMats.Add(new Material(OutlineMat));

                tets.materials = BodyMats.ToArray();
                tets.materials[1].SetFloat("_Scale", Scale);
            }
            else
            {
                if (transform != cookie)
                {
                    List<Material> Mats = new List<Material>();

                    foreach (Material material in tets.materials)
                    {
                        if (!Mats.Contains(material))
                        {
                            Mats.Add(material);
                            Count += 1;
                        }
                    }

                    Mats.Add(new Material(OutlineMat));

                    tets.materials = Mats.ToArray();
                    tets.materials[Count].SetFloat("_Scale", Scale);
                }
            }
        }
    }
}
