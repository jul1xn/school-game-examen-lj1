using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSign : MonoBehaviour
{
    public Animation moveAnim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveAnim.Play();
    }
}
