using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMove : MonoBehaviour
{
    public float moveSpeed = 3f;

    private void Update()
    {
        transform.Translate(new Vector2(0f, (moveSpeed * 100) * Time.deltaTime));
    }
}
