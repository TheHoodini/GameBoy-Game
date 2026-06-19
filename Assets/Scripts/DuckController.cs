using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DuckController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip trickSFX;
    [SerializeField] private AudioClip slideSFX;

    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;

    [Header("Gravity Settings")]
    [SerializeField] private float gravity = 20f;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool isGrounded;
    private bool isSliding = false;
    public bool IsTricking = false;

    private float speedMult = 1f;
    private float airMult = 1f;

    public bool CanMove = true;

    // 1 = normal gravity, -1 = upside-down gravity
    private int gravityDirection = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _animator.SetBool("IsGrounded", isGrounded);
        _animator.SetBool("IsSliding", isSliding);
        if (isGrounded)
        {
            airMult = 1f;
        }
        else
        {
            airMult = 1.5f;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity += Vector2.down * gravity * gravityDirection * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //isGrounded = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!CanMove) return;
        if (context.started && isGrounded)
        {
            _animator.SetTrigger("Jump");
            AudioMgr.Instance.PlaySFX(jumpSFX);
            isSliding = false;

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
        if (!CanMove) return;
        if (!isGrounded) return;

        if (context.started)
        {
            AudioMgr.Instance.PlaySFX(slideSFX);
            isSliding = true;
            boxCollider.enabled = false;
        }

        if (context.canceled)
        {
            boxCollider.enabled = true;
            isSliding = false;
        }
    }

    public void OnTrick(InputAction.CallbackContext context)
    {
        if (!CanMove) return;
        if (context.performed && !IsTricking)
        {
            IsTricking = true;
            AudioMgr.Instance.PlaySFX(trickSFX);
            Debug.Log("Trick");
            _animator.SetTrigger("Trick");
            StartCoroutine(ResetTrick());

            GameManager.Instance.Score += (int)(20 * speedMult * airMult);
        }
    }

    public void OnGravitySwitch(InputAction.CallbackContext context)
    {
        if (!CanMove) return;
        if (!context.performed || !isGrounded) return;

        gravityDirection *= -1;

        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    } 

    public void OnSpeedUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speedMult = 1.5f;
            GameManager.Instance.SpeedMult = 1.3f;
        }

        if (context.canceled && speedMult != 0.5f)
        {
            speedMult = 1f;
            GameManager.Instance.SpeedMult = 1f;
        }
    }

    public void OnSlowDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speedMult = 0.5f;
            GameManager.Instance.SpeedMult = 0.5f;
        }

        if (context.canceled && speedMult != 1.5f)
        {
            speedMult = 1f;
            GameManager.Instance.SpeedMult = 1f;
        }
    }

    public void OnResetPos(InputAction.CallbackContext context)
    {
        transform.position = new Vector3(-2f, -0.5f, -0.5f);
    }

    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    IEnumerator ResetTrick()
    {
        yield return new WaitForSeconds(0.6f);
        IsTricking = false;
    }
}