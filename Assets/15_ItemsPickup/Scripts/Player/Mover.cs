using UnityEngine;

namespace Modul_15
{
    [RequireComponent(typeof(CharacterController))]
    public class Mover : MonoBehaviour
    {
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void MoveCharacter(Vector3 direction, float speed)
        {
            Vector3 moveTo = direction * speed * Time.deltaTime;

            _characterController.Move(moveTo);
        }

        public void RotateCharacter(Vector3 direction, float speed)
        {
            Quaternion lookAt = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAt, speed * Time.deltaTime);
        }
    }
}