using UnityEngine;

public class SphereController : MonoBehaviour
{
    [Header("Settings")]
    public float moveForce = 10f;
    public float rotationSpeed = 100f;

    private Rigidbody _rb;
    private Vector3 _currentForward; // Кешированное направление "вперед"

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        // Инициализируем начальное направление (игнорируя наклон)
        _currentForward = GetFlattenedForward();
    }

    private void Update()
    {
        HandleRotation();
        UpdateForwardDirection();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleRotation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            // Поворот вокруг глобальной оси Y
            Quaternion deltaRotation = Quaternion.Euler(0f, horizontalInput * rotationSpeed * Time.deltaTime, 0f);
            _currentForward = deltaRotation * _currentForward;

            // Применяем поворот к Rigidbody
            _rb.MoveRotation(_rb.rotation * deltaRotation);
        }
    }

    private void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            // Применяем силу в кешированном направлении "вперед"
            Vector3 force = _currentForward * verticalInput * moveForce;
            _rb.AddForce(force, ForceMode.Force);
        }
    }

    private void UpdateForwardDirection()
    {
        // Обновляем направление, но убираем влияние наклона
        _currentForward = GetFlattenedForward();
    }

    private Vector3 GetFlattenedForward()
    {
        // Берем текущий forward, но обнуляем компоненты X и Z вращения,
        // чтобы направление оставалось горизонтальным
        Vector3 flattened = transform.forward;
        flattened.y = 0f; // Обнуляем вертикальную составляющую
        return flattened.normalized;
    }

    // Визуализация для отладки
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, _currentForward * 2f);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * 1.5f);
        }
    }
}