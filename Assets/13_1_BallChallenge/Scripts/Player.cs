using System;
using UnityEngine;

namespace Modul_13_1
{
    [RequireComponent(typeof(Rigidbody), typeof(CoinCollector))]
    public class Player_15 : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const KeyCode JumpKey = KeyCode.Space;

        private const float MoveForce = 600;
        private const float RotateSpeed = 2;
        private const float RotateForce = 90;
        private const float JumpForce = 1000;
        private const float DeadZone = 0.05f;

        private Rigidbody _rigidbody;

        private CoinCollector _coinCollector;
        public event Action OnCoinCollect;

        private float _verticalInput;
        private float _horizontalInput;

        private bool _isJump;
        private bool _isGrounded;

        public Vector3 _cachedForward { get; private set; }
        private float _currentYRotation;

        public int CoinsWallet { get; private set; }

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _coinCollector = GetComponent<CoinCollector>();
            _coinCollector.OnCointCollect += AddCoin;

            _currentYRotation = transform.eulerAngles.y;
            _cachedForward = Quaternion.Euler(0f, _currentYRotation, 0f) * Vector3.forward;
        }

        void Update()
        {
            InputHandler();
        }

        private void FixedUpdate()
        {
            ApplyJump();
            ApplyForces();
        }

        private void InputHandler()
        {
            if (IsGrounded())
            {
                _verticalInput = Input.GetAxisRaw(VerticalAxis);
                _horizontalInput = Input.GetAxisRaw(HorizontalAxis);

                if (_isJump == false)
                    _isJump = Input.GetKeyDown(JumpKey);
            }
        }

        private void ApplyJump()
        {
            if (_isJump)
            {
                _isJump = false;
                _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }

        private void ApplyForces()
        {
            if (Mathf.Abs(_verticalInput) > DeadZone)
            {
                Vector3 moveForce = _cachedForward * _verticalInput * MoveForce;
                _rigidbody.AddForce(moveForce, ForceMode.Force);
            }

            if (Math.Abs(_horizontalInput) > DeadZone)
            {
                _currentYRotation += _horizontalInput * RotateSpeed;
                _cachedForward = Quaternion.Euler(0f, _currentYRotation, 0f) * Vector3.forward;

                Vector3 rotateForce = new Vector3(0, _horizontalInput * RotateForce, 0);
                _rigidbody.AddTorque(rotateForce);
            }
        }

        private void AddCoin()
        {
            CoinsWallet++;
            OnCoinCollect?.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _isGrounded = true;
        }

        private void OnCollisionStay(Collision collision)
        {
            _isGrounded = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            _isGrounded = false;
        }

        private bool IsGrounded()
        {
            return _isGrounded;
        }
    }
}