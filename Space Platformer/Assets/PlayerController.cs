using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D rb;
    public float movementSpeed;
    public float jumpHeight;
    [Space]
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
    bool isGrounded;
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

    public void AddForce(Vector2 force)
    {
        _wantsToAddForce = true;
        _addedForce = force;
    }

    private void Update()
    {
        velocity = rb.velocity;

        float x = Input.GetAxisRaw("Horizontal") * movementSpeed;
        
        if (Input.GetKeyDown(KeyCode.E) && Time.time > dashTime && !isDashing && x != 0f)
        {
            Debug.Log("dash");
            isDashing = true;
            dashTime = Time.time + dashCooldown;
            StartCoroutine(Dash());
        }

        if (!isDashing)
        {
            velocity.x = x;

            isGrounded = CheckGround();

            if (doubleJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    velocity.y = jumpHeight * doubleJumpHeigtMultiplier;
                    doubleJump = false;
                }
            }
            else
            {
                if (isGrounded && Input.GetKeyDown(KeyCode.Space))
                {
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
        Debug.Log("dash start");
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        velocity = new Vector2(rb.velocity.x * dashForce, 0f);
        rb.velocity = velocity;
        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
        Debug.Log("end");
    }
}
