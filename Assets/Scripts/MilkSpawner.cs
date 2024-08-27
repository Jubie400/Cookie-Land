using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MilkSpawner : NetworkBehaviour
{
    public GameObject MilkPrefab;

    public float spawnRadius;
    public int maxMilkCount;
    public float minDistanceBetweenMilks; // Minimum distance between milks

    private void Start()
    {
        SpawnMilk();
    }

    private void SpawnMilk()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, spawnRadius);
        List<Vector3> spawnPoints = new List<Vector3>();

        for (int i = 0; i < maxMilkCount; i++)
        {

            Vector3 spawnPosition = GetValidSpawnPosition(colliders, spawnPoints);
            spawnPoints.Add(spawnPosition);

            GameObject spawnedMilk = Instantiate(MilkPrefab, spawnPosition, MilkPrefab.transform.rotation);
            NetworkServer.Spawn(spawnedMilk);
        }
    }

    private Vector3 GetValidSpawnPosition(Collider[] colliders, List<Vector3> spawnPoints)
    {
        Vector3 randomPoint = Vector3.zero;
        int attempts = 0;

        do
        {
            randomPoint = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPoint.y = 0f; // Ensure milk spawns at ground level
            attempts++;
        }
        while (IsColliding(randomPoint, colliders) || IsTooCloseToOtherSpawn(randomPoint, spawnPoints));

        return randomPoint;
    }

    private bool IsColliding(Vector3 point, Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.bounds.Contains(point))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsTooCloseToOtherSpawn(Vector3 point, List<Vector3> spawnPoints)
    {
        foreach (Vector3 spawnPoint in spawnPoints)
        {
            if (Vector3.Distance(point, spawnPoint) < minDistanceBetweenMilks) // Check against minimum distance
            {
                return true;
            }
        }
        return false;
    }
}
