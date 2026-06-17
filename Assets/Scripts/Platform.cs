using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Lifetime")]
    public float lifetime = 10f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
