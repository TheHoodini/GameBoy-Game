using UnityEngine;

public class Platform : MonoBehaviour
{
    private float speed;
    [SerializeField] private float spawnOffset = 0f;

    private Spawner _spawner;
    [SerializeField] private bool _hasNextSpawned = false;

    void Awake()
    {
        _spawner = FindFirstObjectByType<Spawner>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        speed = GameManager.Instance.Speed;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < 0 && !_hasNextSpawned)
        {
            _spawner.SpawnPrefab(spawnOffset);
            _hasNextSpawned = true;
        }
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
}
