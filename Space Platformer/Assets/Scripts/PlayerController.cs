using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public PlayerUI playerUI;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public float movementSpeed;
    public float jumpHeight;
    public float rgbCycleSpeed;
    public bool rgbPlayer;
    [Space]
    public AudioSource jumpAudio;
    public AudioSource dashAudio;
    public bool doubleJump;
    public float doubleJumpHeigtMultiplier;
    [Space]
    public float dashCooldown;
    public float dashForce;
    public float dashDuration;
    [Space]
    public LayerMask groundLayer;
    public float groundCheckOffset;
    public float groundCheckRadius;

    float dashTime;
    bool isDashing;
    public bool isGrounded { get; private set; }
    bool wasGrounded;
    Vector2 velocity;
    Collider2D selfCollider;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dashTime = Time.time;
        selfCollider = GetComponent<Collider2D>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - groundCheckOffset, transform.position.z), groundCheckRadius);
    }

    Vector2 _addedForce;
    bool _wantsToAddForce;

    public void ThrowPlayer(float throwForce)
    {
        Vector2 force = new Vector2(Mathf.Clamp01(velocity.x) * throwForce, throwForce / 2);
        AddForce(force);
    }

    public void AddForce(Vector2 force)
    {
        _wantsToAddForce = true;
        _addedForce = force;
    }

    private void Update()
    {
        velocity = rb.velocity;
        if (rgbPlayer)
        {
            spriteRenderer.color = GetCyclingColor(Time.time * rgbCycleSpeed);
        }
        else
        {
            spriteRenderer.color = Color.white;
        }

        if (transform.position.y < -6)
        {
            playerUI.KillPlayer();
        }

        float x = Input.GetAxisRaw("Horizontal") * movementSpeed;
        animator.SetFloat("magnitude", rb.velocity.magnitude);
        
        if (Input.GetKeyDown(KeyCode.E) && Time.time > dashTime && !isDashing && x != 0f)
        {
            dashAudio.Play();
            isDashing = true;
            dashTime = Time.time + dashCooldown;
            StartCoroutine(Dash());
        }

        if (!isDashing)
        {
            velocity.x = x;
            if (x != 0f)
            {
                spriteRenderer.flipX = x < 0f;
            }

            isGrounded = CheckGround();
            animator.SetBool("grounded", isGrounded);

            if (doubleJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumpAudio.Play();
                    StartCoroutine(DoubleJumpSequence());
                    velocity.y = jumpHeight * doubleJumpHeigtMultiplier;
                    doubleJump = false;
                }
            }
            else
            {
                if (isGrounded && Input.GetKeyDown(KeyCode.Space))
                {
                    jumpAudio.Play();
                    velocity.y = jumpHeight;
                    doubleJump = true;
                }
            }
        }

        if (!wasGrounded && isGrounded)
        {
            doubleJump = false;
        }

        wasGrounded = isGrounded;

        if (_wantsToAddForce)
        {
            _wantsToAddForce = false;
            velocity += _addedForce;
        }

        rb.velocity = velocity;
    }

    private IEnumerator DoubleJumpSequence()
    {
        animator.SetBool("doublejump", true);
        yield return new WaitForEndOfFrame();
        animator.SetBool("doublejump", false);
    }

    private bool CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y - groundCheckOffset), groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            return true;
        }

        return false;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        velocity = new Vector2(rb.velocity.x * dashForce, 0f);
        rb.velocity = velocity;
        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private Color GetCyclingColor(float t)
    {
        float r = Mathf.Sin(t + 0f) * 0.5f + 0.5f;
        float g = Mathf.Sin(t + 2f) * 0.5f + 0.5f;
        float b = Mathf.Sin(t + 4f) * 0.5f + 0.5f;

        return new Color(r, g, b);
    }
}
