using UnityEngine;

namespace Modul_25.Gameplay
{
    public abstract class Mover
    {
        private const float MinAngleToRotate = 0.05f;

        private float _moveSpeed;
        private float _rotateSpeed;

        protected Vector3 _currentMoveVector;

        public Mover(float moveSpeed, float rotateSpeed)
        {
            _moveSpeed = moveSpeed;
            _rotateSpeed = rotateSpeed;
        }

        public bool IsMoved { get; private set; }
        public Vector3 CurrentVelocity => _currentMoveVector.normalized * _moveSpeed;
        public abstract Quaternion CurrentRotation { get; }

        public void Update(float deltaTime)
        {
            Move(deltaTime);
            Rotate(deltaTime);

            IsMoved = CurrentVelocity != Vector3.zero;
        }

        public void SetMoveDirection(Vector3 moveVector) => _currentMoveVector = moveVector;

        protected abstract void Move(float deltaTime);

        protected void Rotate(float deltaTime)
        {
            if (_currentMoveVector.magnitude < MinAngleToRotate)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_currentMoveVector.normalized);
            float step = _rotateSpeed * deltaTime;
            ApplyRotation(Quaternion.RotateTowards(CurrentRotation, lookRotation, step));
        }

        protected abstract void ApplyRotation(Quaternion rotation);
    }
}