
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _mainPlayerTransform;
    [SerializeField] float _smoothSpeed = 0.125f;

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 desiredPosition = _mainPlayerTransform.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }
}
