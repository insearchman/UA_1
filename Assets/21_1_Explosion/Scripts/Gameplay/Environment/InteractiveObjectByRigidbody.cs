using UnityEngine;

namespace Modul_21_1.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractiveObjectByRigidbody : MonoBehaviour, IDragable, IExplosionInteractable
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void ExplosionInteract(Vector3 explosionPower)
        {
            _rigidbody.AddForce(explosionPower);
        }

        public void OnGrab()
        {
            _rigidbody.isKinematic = true;
        }

        public void OnDrag(Vector3 position)
        {
            transform.position = position;
        }

        public void OnRelease()
        {
            _rigidbody.isKinematic = false;
        }
    }
}