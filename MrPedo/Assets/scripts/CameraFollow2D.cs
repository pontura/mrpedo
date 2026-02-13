using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow Settings")]
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;

    [Header("Inertia")]
    [SerializeField] private float smoothTimeX = 10f;
    [SerializeField] private float smoothTimeY = 50f;

    [Header("Offset")]
    [SerializeField] private Vector3 offset = new Vector3(3f, 0f, -10f);

    private float velocityX;
    private float velocityY;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 currentPosition = transform.position;

        float newX = currentPosition.x;
        float newY = currentPosition.y;

        if (followX)
        {
            newX = Mathf.SmoothDamp(
                currentPosition.x,
                desiredPosition.x,
                ref velocityX,
                smoothTimeX * Time.deltaTime
            );
        }

        if (followY)
        {
            newY = Mathf.SmoothDamp(
                currentPosition.y,
                desiredPosition.y,
                ref velocityY,
                smoothTimeY * Time.deltaTime
            );
        }

        transform.position = new Vector3(newX, newY, currentPosition.z);
    }
}
