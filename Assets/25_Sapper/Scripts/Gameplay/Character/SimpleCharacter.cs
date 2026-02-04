using UnityEngine;

namespace Modul_25.Gameplay
{
    public class SimpleCharacter : Character
    {
        protected override void Awake()
        {
            if (TryGetComponent(out CharacterController characterController))
                _mover = new CharacterControllerMover(characterController, transform, MoveSpeed, RotateSpeed);
            else if (TryGetComponent(out Rigidbody rigidbody))
                _mover = new RigidbodyMover(rigidbody, MoveSpeed, RotateSpeed);
            else
                Debug.Log("No move component");

            base.Awake();
        }
    }
}