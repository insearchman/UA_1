using UnityEngine;

public class Game_BallMaze : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const float DeadZone = 0.05f;
    private const float RotateSpeed = 50;

    [SerializeField] private Transform _maze;
    [SerializeField] private Rigidbody _playerRigidbody;

    private float _horizontalInput;
    private float _verticalInput;

    private void Start()
    {
        _playerRigidbody.sleepThreshold = 0f;
    }

    private void Update()
    {
        InputHandler();
    }

    private void FixedUpdate()
    {
        ApplyRotation();
    }

    private void InputHandler()
    {
        _horizontalInput = Input.GetAxis(HorizontalAxis);
        _verticalInput = Input.GetAxis(VerticalAxis);
    }

    private void ApplyRotation()
    {
        if(Mathf.Abs(_horizontalInput) > DeadZone || Mathf.Abs(_verticalInput) > DeadZone)
        {
            _maze.Rotate(-_horizontalInput * RotateSpeed * Time.deltaTime, 0, -_verticalInput * RotateSpeed * Time.deltaTime);
        }
    }
}
