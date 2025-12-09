using UnityEngine;

namespace Modul_13_1
{
    public class AutoBalance : MonoBehaviour
    {
        private const float ReturnStrength = 100f;
        private const float MinAngle = 0.5f;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            float currentTilt = transform.eulerAngles.x;

            if (Mathf.Abs(currentTilt) > MinAngle)
            {
                float returnTorque = -currentTilt * ReturnStrength * Time.fixedDeltaTime;
                returnTorque = Mathf.Clamp(returnTorque, -ReturnStrength, ReturnStrength);

                _rigidbody.AddTorque(returnTorque, 0, 0, ForceMode.Force);
            }
        }
    }
}