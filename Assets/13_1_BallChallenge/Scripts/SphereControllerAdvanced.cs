using UnityEngine;

public class SphereControllerAdvanced : MonoBehaviour
{
    [Header("Settings")]
    public float moveForce = 10f;
    public float rotationSpeed = 100f;
    public float maxAngularVelocity = 10f;

    private Rigidbody _rb;
    private Vector3 _cachedForward;
    private float _currentYRotation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.maxAngularVelocity = maxAngularVelocity;

        // Инициализируем начальные значения
        _currentYRotation = transform.eulerAngles.y;
        _cachedForward = Quaternion.Euler(0f, _currentYRotation, 0f) * Vector3.forward;
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        ApplyForces();
    }

    private void HandleInput()
    {
        // Обработка поворота
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            _currentYRotation += horizontalInput * rotationSpeed * Time.deltaTime;
            _cachedForward = Quaternion.Euler(0f, _currentYRotation, 0f) * Vector3.forward;
        }
    }

    private void ApplyForces()
    {
        // Движение вперед/назад
        float verticalInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            Vector3 moveForceVector = _cachedForward * verticalInput * moveForce;
            _rb.AddForce(moveForceVector, ForceMode.Force);
        }

        // Дополнительно: применяем крутящий момент для лучшего качения
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            // Крутящий момент для качения (перпендикулярно движению)
            Vector3 torque = Vector3.Cross(Vector3.up, _cachedForward) * verticalInput * moveForce * 0.5f;
            _rb.AddTorque(torque, ForceMode.Force);
        }
    }
}