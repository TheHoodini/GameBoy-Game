using UnityEngine;

public class AuraBox : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Duck"))
        {
            GameManager.Instance.Score += 50;
        }
    }

    private void OnDrawGizmos()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col == null) return;

        Gizmos.color = Color.green;

        Vector3 center = transform.TransformPoint(col.offset);
        Vector3 size = new Vector3(
            col.size.x * transform.lossyScale.x,
            col.size.y * transform.lossyScale.y,
            0f
        );

        Gizmos.DrawWireCube(center, size);
    }
}