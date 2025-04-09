using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public EnemyController enemyController;
    public AudioSource shootAudio;
    public GameObject bulletPrefab;
    public float shootDelay = 0.5f;
    public float shootAnimationDurationInFrames;

    private float lastShot;

    private void Start()
    {
        lastShot = Time.time;
    }

    private void Update()
    {
        if (enemyController.isDead) return; // Don't shoot if dead

        if (enemyController.CheckRadius())
        {
            float direction = PlayerController.instance.transform.position.x > transform.position.x ? 1 : -1;
            enemyController.spriteRenderer.flipX = direction == 1;

            if (Time.time > lastShot)
            {
                lastShot = Time.time + shootDelay;
                StartCoroutine(ShootSequence());
            }
        }
    }

    private IEnumerator ShootSequence()
    {
        enemyController.animator.SetBool("shoot", true);

        yield return new WaitForSeconds(0.35f); // Time before shot

        shootAudio.Play();
        Vector3 instantiatePosition = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z);
        Instantiate(bulletPrefab, instantiatePosition, Quaternion.identity);

        // Wait for rest of the animation
        float totalDuration = shootAnimationDurationInFrames / 60f;
        yield return new WaitForSeconds(Mathf.Max(0, totalDuration - 0.35f));

        enemyController.animator.SetBool("shoot", false);
    }
}
