using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float baseLerpTime = 0.05f;
    public float maxLerpTime = 0.3f;
    public float minLerpTime = 0.01f;
    public float xOffset = 2f;
    public float yOffset = 1.5f;

    private Vector3 targetPos;

    private void Start()
    {
        targetPos = transform.position;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Calculate offset bounds
        float minX = transform.position.x - xOffset;
        float maxX = transform.position.x + xOffset;
        float minY = transform.position.y - yOffset;
        float maxY = transform.position.y + yOffset;

        // Update targetPos only if out of bounds
        if (target.position.x < minX || target.position.x > maxX)
        {
            targetPos.x = target.position.x;
        }

        if (target.position.y < minY || target.position.y > maxY)
        {
            targetPos.y = target.position.y;
        }

        targetPos.z = transform.position.z;

        // Calculate distance and scale lerpTime
        float distance = Vector3.Distance(transform.position, targetPos);
        float dynamicLerpTime = Mathf.Clamp(baseLerpTime + distance * 0.05f, minLerpTime, maxLerpTime);

        // Smoothly move toward target
        transform.position = Vector3.Lerp(transform.position, targetPos, dynamicLerpTime * Time.deltaTime);
    }
}
