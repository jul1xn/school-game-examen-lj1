using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public string playerTag = "Player";
    public float standDuration;
    public float wiggleRadius;
    public float wiggleDelay;
    bool wiggle;
    bool stoodOn;
    float lastWiggle;
    Vector2 basePos;
    Collider2D platformCollider;

    private void Start()
    {
        lastWiggle = Time.time;
        wiggle = false;
        stoodOn = false;
        basePos = transform.position;
        platformCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (wiggle && Time.time > lastWiggle)
        {
            lastWiggle = Time.time + wiggleDelay;
            Vector2 newPos = new Vector2(Random.Range(basePos.x - wiggleRadius, basePos.x + wiggleRadius), basePos.y);
            transform.position = newPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            StartCoroutine(Sequence());
        }
    }

    private IEnumerator Sequence()
    {
        if (stoodOn)
        {
            yield return null;
        }

        stoodOn = true;

        wiggle = true;
        yield return new WaitForSeconds(standDuration);
        wiggle = false;

        animator.SetBool("off", true);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        platformCollider.enabled = false;
    }
}
