using UnityEngine;

public class CameraFollower : MonoBehaviour
{


    [SerializeField] private Transform _target;

    private Vector3 _offset;
    private float _currentDistance;

    private float _currentCameraRotationY;
    private Vector3 _lastValidForwardDirection;

    private void Start()
    {
        _offset = transform.position - _target.transform.position;
        _currentDistance = _offset.magnitude;
        _lastValidForwardDirection = _target.forward;
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Rigidbody targetRb = _target.GetComponent<Rigidbody>();

        if (targetRb.velocity.magnitude > 0.1f)
        {
            _lastValidForwardDirection = targetRb.velocity.normalized;
        }

        float targetYRotation = Mathf.Atan2(_lastValidForwardDirection.x, _lastValidForwardDirection.z) * Mathf.Rad2Deg;

        _currentCameraRotationY = Mathf.LerpAngle(_currentCameraRotationY, targetYRotation, 2f * Time.deltaTime);

        Quaternion rotation = Quaternion.Euler(0, _currentCameraRotationY, 0);
        Vector3 cameraPosition = _target.position + rotation * (_offset.normalized * _currentDistance);

        transform.position = cameraPosition;
        transform.LookAt(_target.position);
    }
}