using UnityEngine;

namespace Modul_23.Gameplay
{
    public class CharacterMover
    {
        private const float MinAngleToRotate = 0.05f;

        private readonly float MoveSpeed;
        private readonly float RotateSpeed;

        private readonly CharacterController CharacterController;
        private readonly Transform MovableTransform;

        private Vector3 _currentDirection;

        public CharacterMover(CharacterController characterController, Transform transform, float moveSpeed, float rotateSpeed)
        {
            CharacterController = characterController;
            MovableTransform = transform;
            MoveSpeed = moveSpeed;
            RotateSpeed = rotateSpeed;
        }

        public bool IsMoved { get; private set; }
        public Vector3 CurrentVelocity { get; private set; }

        public void Update(float deltaTime)
        {
            Move(deltaTime);
            Rotate(deltaTime);
        }

        public void SetPointToMove(Vector3 direction) => _currentDirection = direction;

        private void Move(float deltaTime)
        {
            CurrentVelocity = _currentDirection.normalized * MoveSpeed;

            CharacterController.Move(CurrentVelocity * deltaTime);

            if (CurrentVelocity == Vector3.zero)
                IsMoved = false;
            else
                IsMoved = true;
        }

        private void Rotate(float deltaTime)
        {
            if (_currentDirection.magnitude < MinAngleToRotate)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_currentDirection.normalized);

            float step = RotateSpeed * deltaTime;

            MovableTransform.rotation = Quaternion.RotateTowards(MovableTransform.rotation, lookRotation, step);
        }
    }
}