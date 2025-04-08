using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float lerpTime = 0.3f;

    float minY;
    float minX;

    private void Start()
    {
        minY = transform.position.y;
        minX = transform.position.x;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        if (targetPos.y < minY)
        {
            targetPos.y = minY;
        }

        if (targetPos.x < minX)
        {
            targetPos.x = minX;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, lerpTime * Time.deltaTime);
    }
}
