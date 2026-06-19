using UnityEngine;

public class MovingBg : MonoBehaviour
{
    private float speed;
    [SerializeField] private float disappearX = -11f;

    private void Start()
    {
        
    }
    void Update()
    {
        speed = GameManager.Instance.Speed / 3;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < disappearX)
        {
            transform.position = new Vector3(11.2f, transform.position.y, transform.position.z);
        }
    }
}
