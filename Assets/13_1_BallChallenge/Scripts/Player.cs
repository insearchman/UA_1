using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const KeyCode JumpKey = KeyCode.Space;
    private const float DeadZone = 0.05f;

    [SerializeField] private float _moveForce;
    [SerializeField] private float _rotateForce;
    [SerializeField] private float _jumpForce;

    private float _moveInput;
    private float _turnInput;
    private bool _isJump;
    private bool _isGrounded;
    private Vector3 horizontalForward;
    private Vector3 _moveDirection;

    private Vector3 _cachedForward;
    private float _currentYRotation;
    public float maxAngularVelocity = 10f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = maxAngularVelocity;

        _currentYRotation = transform.eulerAngles.y;
        _cachedForward = Quaternion.Euler(0f, _currentYRotation, 0f) * Vector3.forward;
    }

    void Update()
    {
        if (IsGrounded())
        {
            _moveInput = Input.GetAxisRaw(VerticalAxis);
            _turnInput = Input.GetAxisRaw(HorizontalAxis);

            if (_isJump == false)
                _isJump = Input.GetKeyDown(JumpKey);
        }

        HandleInput();
    }

    private void FixedUpdate()
    {
        if (_isJump)
        {
            _isJump = false;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        ApplyForces();

        if (Math.Abs(_turnInput) > DeadZone)
        {
            Vector3 turnTorque = Vector3.up * _turnInput;
            _moveDirection = turnTorque;
            _rigidbody.AddTorque(turnTorque);
        }
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            _currentYRotation += horizontalInput * _rotateForce * Time.deltaTime;
            _cachedForward = Quaternion.Euler(0f, _currentYRotation, 0f) * Vector3.forward;
        }
    }

    private void ApplyForces()
    {
        float verticalInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            Vector3 moveForceVector = _cachedForward * verticalInput * _moveForce;
            _rigidbody.AddForce(moveForceVector, ForceMode.Force);
        }

        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            Vector3 torque = Vector3.Cross(Vector3.up, _cachedForward) * verticalInput * _moveForce * 0.5f;
            _rigidbody.AddTorque(torque, ForceMode.Force);
        }
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