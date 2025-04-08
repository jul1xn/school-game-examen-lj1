using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherries : Collectible
{
    public float movementMultiplier;
    public float duration;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnPickup()
    {
        StartCoroutine(Sequence());
    }

    public IEnumerator Sequence()
    {
        PlayerController.instance.rgbPlayer = true;

        float oldSpeed = PlayerController.instance.movementSpeed;
        float oldJump = PlayerController.instance.jumpHeight;
        PlayerController.instance.movementSpeed = oldSpeed * movementMultiplier;
        PlayerController.instance.jumpHeight = oldJump * movementMultiplier;

        yield return new WaitForSeconds(deathAnimationTime);
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(duration - deathAnimationTime);
        PlayerController.instance.movementSpeed = oldSpeed;
        PlayerController.instance.jumpHeight = oldJump;

        PlayerController.instance.rgbPlayer = false;
        Destroy(gameObject);
    }
}
