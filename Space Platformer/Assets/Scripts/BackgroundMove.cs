using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float moveSpeed;
    public float maxX;
    float startX;
    float currentX;

    private void Start()
    {
        currentX = 0;
        startX = transform.position.x;
    }

    private void Update()
    {
        currentX += moveSpeed * Time.deltaTime;
        if (currentX > maxX)
        {
            currentX = 0;
        }

        transform.position = new Vector3(startX + currentX, Camera.main.transform.position.y);
    }
}
