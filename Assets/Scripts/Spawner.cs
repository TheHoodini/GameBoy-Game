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
            //SpawnPrefab();
            timer = 0f;
        }
    }

    public void SpawnPrefab(float offset = 0)
    {
        if (prefabs.Length == 0) return;

        int randomIndex = Random.Range(0, prefabs.Length);

        Vector3 newPos = transform.position;
        newPos.x += offset;

        Instantiate(
            prefabs[randomIndex],
            newPos,
            transform.rotation
        );
    }
}