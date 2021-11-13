using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothing = 2f;

    [SerializeField] bool lockVerticalMovement;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;

        if (lockVerticalMovement)
        {
            targetPosition.y = transform.position.y;
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform cameraTarget)
    {
        this.target = cameraTarget;
    }
}
