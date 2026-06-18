using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DuckController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;

    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;

    [Header("Gravity Settings")]
    [SerializeField] private float gravity = 20f;

    private Rigidbody2D rb;
    private bool isGrounded;

    // 1 = normal gravity, -1 = upside-down gravity
    private int gravityDirection = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        _animator.SetBool("IsGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity += Vector2.down * gravity * gravityDirection * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            _animator.SetTrigger("Jump");

            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce * gravityDirection
            );

            isGrounded = false;
        }

        if (context.canceled)
        {
            if (rb.linearVelocity.y * gravityDirection > 0)
            {
                rb.linearVelocity = new Vector2(
                    rb.linearVelocity.x,
                    rb.linearVelocity.y * jumpCutMultiplier
                );
            }
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
            Debug.Log("Trick");
            _animator.SetTrigger("Trick");
        }
    }

    public void OnGravitySwitch(InputAction.CallbackContext context)
    {
        if (!context.performed || !isGrounded) return;

        gravityDirection *= -1;

        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }

    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}