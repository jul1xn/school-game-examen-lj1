using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string playerTag = "Player";
    public Animator animator;
    public float deathAnimationTime;
    public bool destroyOnAnimationEnd = true;
    private bool pickedUp = false;
    public AudioSource pickupSource;

    private void Start()
    {
        if (pickupSource == null)
        {
            pickupSource = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && !pickedUp)
        {
            OnPickup();
            StartCoroutine(PickupSequence());
        }
    }

    public virtual void OnPickup()
    {
        // Override this function in other scripts :P
    }

    private IEnumerator PickupSequence()
    {
        pickedUp = true;
        pickupSource.Play();
        animator.SetBool("pickedUp", true);
        yield return new WaitForSeconds(deathAnimationTime);
        if (destroyOnAnimationEnd)
        {
            Destroy(gameObject);
        }
    }
}
