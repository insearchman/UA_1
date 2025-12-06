using UnityEngine;

namespace Modul_17
{
    [RequireComponent(typeof(CharacterController))]
    public class Mover : MonoBehaviour
    {
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void MoveCharacter(Vector3 direction, float moveSpeed, float rotateSpeed)
        {
            Vector3 moveTo = direction * moveSpeed * Time.deltaTime;
            Quaternion lookAt = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAt, rotateSpeed * Time.deltaTime);
            _characterController.Move(moveTo);
        }
    }
}