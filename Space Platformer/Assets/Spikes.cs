using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float throwForce;
    public string playerTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            PlayerController.instance.ThrowPlayer(throwForce);
            PlayerController.instance.playerUI.TakeAwayLife();
        }
    }
}
