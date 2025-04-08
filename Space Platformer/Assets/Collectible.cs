using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string playerTag = "Player";
    public Animator animator;
    public float deathAnimationTime;
    public bool pickedUp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && !pickedUp)
        {
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
        OnPickup();
        animator.SetBool("pickedUp", true);
        yield return new WaitForSeconds(deathAnimationTime);
        Destroy(gameObject);
    }
}
