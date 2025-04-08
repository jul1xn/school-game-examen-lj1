using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    [Space]
    public string playerTag;
    public float playerThrowForce;
    [Space]
    public float sweepRange;
    public float detectionRange;
    public float forwardsDetectionOffset;
    [Space]
    public LayerMask groundMask;
    public float groundDetectionRange;
    public float groundDetectionOffset;
    [Space]
    public LayerMask stompLayer;
    public float stompOffset;
    public float stompWidth;
    public float stompHeight;
    public float playerStompJumpForce;
    public float deathAnimationTime;
    [Space]
    public float movementSpeed;

    float direction;
    private float boundMinX;
    private float boundMaxX;
    bool isDead;

    Vector3 gizmoDrawPosition;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + (direction * forwardsDetectionOffset), transform.position.y - groundDetectionOffset, transform.position.z), groundDetectionRange);

        Gizmos.color = Color.red;
        if (Application.isPlaying)
        {
            Gizmos.DrawWireCube(gizmoDrawPosition, new Vector3(sweepRange*2, transform.localScale.y, transform.localScale.z));
        }
        else
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(sweepRange*2, transform.localScale.y, transform.localScale.z));
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + stompOffset, transform.position.z), new Vector3(stompWidth, stompHeight, transform.localScale.z));
    }

    private void Start()
    {
        isDead = false;
        gizmoDrawPosition = transform.position;

        // Calculate bounds
        boundMinX = transform.position.x - sweepRange;
        boundMaxX = transform.position.x + sweepRange;

        direction = 1;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (StompCheck())
            {
                StartCoroutine(DeathSequence());
            }

            if (transform.position.x + forwardsDetectionOffset > boundMaxX ||
                transform.position.x - forwardsDetectionOffset < boundMinX ||
                !CheckGround())
            {
                direction *= -1;
            }

            animator.SetFloat("magnitude", rb.velocity.magnitude);
            spriteRenderer.flipX = direction == 1;

            rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag) && !StompCheck())
        {
            PlayerController.instance.ThrowPlayer(playerThrowForce);
            PlayerController.instance.playerUI.TakeAwayLife();
        }
    }

    private IEnumerator DeathSequence()
    {
        isDead = true;
        animator.SetBool("dead", true);
        PlayerController.instance.AddForce(new Vector2(0f, playerStompJumpForce));
        yield return new WaitForSeconds(0.2f); // Depends on the death animation time
        Destroy(gameObject);
    }

    private bool StompCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + stompHeight), new Vector2(stompWidth, stompHeight), 0f, stompLayer);
        if (colliders.Length > 0)
        {
            return true;
        }

        return false;
    }

    private bool CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + (direction * forwardsDetectionOffset), transform.position.y - groundDetectionOffset), groundDetectionRange, groundMask);
        if (colliders.Length > 0)
        {
            return true;
        }

        return false;
    }

}
