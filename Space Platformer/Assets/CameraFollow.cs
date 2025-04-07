using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float lerpTime = 0.3f;

    float minY;

    private void Start()
    {
        minY = transform.position.y;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        if (targetPos.y < minY)
        {
            targetPos.y = minY;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, lerpTime * Time.deltaTime);
    }
}
