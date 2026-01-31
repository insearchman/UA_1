using UnityEngine;

namespace Modul_25.Gameplay
{
    public class CharacterControllerMover : Mover
    {
        private CharacterController _characterController;
        private Transform _transform;

        public CharacterControllerMover(CharacterController characterController, Transform transform, float moveSpeed, float rotateSpeed) 
            : base (moveSpeed, rotateSpeed)
        {
            _characterController = characterController;
            _transform = transform;
        }

        public override Quaternion CurrentRotation => _transform.rotation;

        protected override void ApplyRotation(Quaternion rotation) => _transform.rotation = rotation;

        protected override void Move(float deltaTime) => _characterController.Move(CurrentVelocity * deltaTime);
    }
}