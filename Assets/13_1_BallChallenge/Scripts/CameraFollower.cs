using UnityEngine;

namespace Modul_13_1
{
    public class CameraFollower : MonoBehaviour
    {
        private const float DeadZone = 0.05f;

        [SerializeField] private Transform _target;

        private Rigidbody targetRigidbody;

        private Vector3 _offset;
        private Vector3 _defautAngle;
        private float _currentCameraRotationY;
        private Vector3 _lastValidForwardDirection;

        private void Start()
        {
            targetRigidbody = _target.GetComponent<Rigidbody>();

            _offset = transform.position - _target.transform.position;
            _defautAngle.x = transform.rotation.eulerAngles.x;
            _lastValidForwardDirection = _target.forward;
        }

        private void Update()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            if (targetRigidbody.velocity.magnitude > DeadZone)
            {
                _lastValidForwardDirection = targetRigidbody.velocity.normalized;
            }

            float targetYRotation = Mathf.Atan2(_lastValidForwardDirection.x, _lastValidForwardDirection.z) * Mathf.Rad2Deg;

            _currentCameraRotationY = Mathf.LerpAngle(_currentCameraRotationY, targetYRotation, Time.deltaTime);

            Quaternion rotation = Quaternion.Euler(0, _currentCameraRotationY, 0);
            Vector3 cameraPosition = _target.position + rotation * _offset;

            transform.position = cameraPosition;
            transform.LookAt(_target.position);
            transform.rotation = transform.rotation * Quaternion.Euler(-_defautAngle);
        }
    }
}