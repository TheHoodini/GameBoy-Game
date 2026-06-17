using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs list")]
    public GameObject[] prefabs;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPrefab();
            timer = 0f;
        }
    }

    void SpawnPrefab()
    {
        if (prefabs.Length == 0) return;

        int randomIndex = Random.Range(0, prefabs.Length);

        Instantiate(
            prefabs[randomIndex],
            transform.position,
            transform.rotation
        );
    }
}