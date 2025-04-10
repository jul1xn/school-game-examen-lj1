using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLogic : MonoBehaviour
{
    public Animator animator;
    public GameObject failedText;
    public string playerTag = "Player";
    public bool ended;

    private void Start()
    {
        failedText.SetActive(false);
        ended = false;
    }

    private void Update()
    {
        if (ended)
        {
            ended = false;
            SceneLoader.Instance.LoadScene(6);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            if (SaveManager.instance.HasFoundAllParts())
            {
                PlayerController.instance.gameObject.SetActive(false);
                animator.SetBool("ending", true);
            }
            else
            {
                failedText.SetActive(true);
            }
        }
    }
}
