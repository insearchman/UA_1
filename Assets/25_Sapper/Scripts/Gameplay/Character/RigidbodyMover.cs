using UnityEngine;

namespace Modul_25.Gameplay
{
    public class RigidbodyMover : Mover
    {
        private Rigidbody _rigidbody;

        public RigidbodyMover(Rigidbody rigidbody, float moveSpeed, float rotateSpeed) 
            : base(moveSpeed, rotateSpeed)
        {
            _rigidbody = rigidbody;
        }

        public override Quaternion CurrentRotation => _rigidbody.rotation;

        protected override void ApplyRotation(Quaternion rotation) => _rigidbody.MoveRotation(rotation);

        protected override void Move(float deltaTime) => _rigidbody.velocity = CurrentVelocity;
    }
}