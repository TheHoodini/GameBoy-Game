using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Laser hit: " + other.gameObject.name);
        if (other.CompareTag("Duck"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
