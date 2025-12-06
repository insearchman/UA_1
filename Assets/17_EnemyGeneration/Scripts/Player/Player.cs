using UnityEngine;

namespace Modul_17
{
    [RequireComponent(typeof(Mover), typeof(InputKeyboard))]
    public class Player : MonoBehaviour
    {
        private InputSystem _input;
        private Mover _mover;
        private CameraController _mainCamera;

        public float WalkingSpeed { get; private set; } = 1.5f;
        public float RunningSpeed { get; private set; } = 5f;
        public float RotationSpeed { get; private set; } = 500f;

        private void Start()
        {
            _input = GetComponent<InputSystem>();
            _input.OnMove += MoveHandler;

            _mover = GetComponent<Mover>();

            _mainCamera = new();
            _mainCamera.InitCamera(transform.position);
        }

        private void MoveHandler(Vector3 normalizedinput)
        {
            if (_input.IsRunning)
            {
                _mover.MoveCharacter(normalizedinput, RunningSpeed, RotationSpeed);
            }
            else
            {
                _mover.MoveCharacter(normalizedinput, WalkingSpeed, RotationSpeed);
            }

            _mainCamera.Foloww(transform.position);
        }
    }
}