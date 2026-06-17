using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DuckController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [Header("Settings")]
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Awake()
    {
        //_animator.SetTrigger("Start");
        //_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        _animator.SetBool("IsGrounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            _animator.SetTrigger("Jump");
            StartCoroutine(WaitSeconds(0.3f)); 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
        
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            _animator.SetTrigger("Slide");
        }
    }

    public void OnTrick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _animator.SetTrigger("Trick");
        }
    }

    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
