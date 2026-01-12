using System;
using UnityEngine;

public class GameSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject starPrefab;
    public GameObject applePrefab;

    public float spawnInterval = 1.5f;
    private float timer;

    // Heights (Adjust these based on your game view)
    public float floorY = -3.5f;
    public float airY = -1.0f; // High enough to need a jump

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0;
        }
    }

    void Spawn()
    {
        int rng = Random.Range(0, 100);
        GameObject obj = null;
        Vector3 pos = Vector3.zero;

        // 40% Chance Obstacle
        if (rng < 40)
        {
            obj = obstaclePrefab;
            pos = new Vector3(12, floorY, 0);
        }
        // 30% Chance Star (Floor)
        else if (rng < 70)
        {
            obj = starPrefab;
            pos = new Vector3(12, floorY, 0);
        }
        // 30% Chance Apple (Air)
        else
        {
            obj = applePrefab;
            pos = new Vector3(12, airY, 0);
        }

        GameObject newObj = Instantiate(obj, pos, Quaternion.identity);

        // Attach mover script dynamically
        newObj.AddComponent<ObjectMover>();
    }
}