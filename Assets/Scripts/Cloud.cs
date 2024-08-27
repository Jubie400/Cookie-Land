using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject[] Clouds;
    public int Count;
    public int Total;
    public bool Done;

    void Start()
    {

    }

    void Update()
    {
        if (gameObject.name.Contains("Clouds"))
        {
            Vector3 center = GetComponents<Collider>()[0].bounds.center;
            Vector3 size = GetComponents<Collider>()[0].bounds.size;
            Vector3 center2 = GetComponents<Collider>()[1].bounds.center;
            Vector3 size2 = GetComponents<Collider>()[1].bounds.size;

            // Continue spawning clouds until Count is greater than or equal to Total
            while (Count < Total)
            {
                Vector3 randomPosition;

                if (!Done)
                {
                    randomPosition = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
                }
                else
                {
                    randomPosition = center2 + new Vector3(Random.Range(-size2.x / 2, size2.x / 2), Random.Range(-size2.y / 2, size2.y / 2), Random.Range(-size2.z / 2, size2.z / 2));
                }

                int cloudObject = Random.Range(0, Clouds.Length);
                GameObject newCloud = Instantiate(Clouds[cloudObject], randomPosition, Quaternion.identity);
                newCloud.name = "Cloud" + Count;
                float randomScale = Random.Range(1, 3f);
                newCloud.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                newCloud.transform.SetParent(transform);

                // Check if the new cloud is too close to other clouds
                bool positionIsValid = true;
                foreach (Transform cloud in transform)
                {
                    if (Vector3.Distance(randomPosition, cloud.position) < 10 && cloud != newCloud.transform)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                // If position is not valid, move it again
                if (!positionIsValid)
                {
                    newCloud.transform.position = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
                }

                Count += 1;
            }

            foreach (Transform cloud in transform)
            {
                if (transform.name == "Clouds" && cloud.transform.localPosition.x > 0.5f || transform.name == "Clouds3" && cloud.transform.localPosition.x > 0.5f)
                {
                    Destroy(cloud.gameObject);
                    Count -= 1;
                }

                if (transform.name == "Clouds2" && cloud.transform.localPosition.x < -0.5f)
                {
                    Destroy(cloud.gameObject);
                    Count -= 1;
                }
            }
        }

        // Move clouds if Count is greater than or equal to Total
        if (Count >= Total)
        {
            Done = true;
            foreach (Transform cloud in transform)
            {
                if (transform.name == "Clouds" || transform.name == "Clouds3")
                {
                    cloud.transform.Translate(Vector3.right * 0.5f * Time.deltaTime);
                }

                if (transform.name == "Clouds2")
                {
                    cloud.transform.Translate(Vector3.left * 0.5f * Time.deltaTime);
                }
            }
        }
    }
}

