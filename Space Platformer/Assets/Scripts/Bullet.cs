using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public string playerTag = "Player";
    public float movementSpeed = 5f;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector3 dir = PlayerController.instance.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rb.velocity = transform.right * 5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            PlayerController.instance.playerUI.TakeAwayLife();
        }

        Destroy(gameObject);
    }
}
