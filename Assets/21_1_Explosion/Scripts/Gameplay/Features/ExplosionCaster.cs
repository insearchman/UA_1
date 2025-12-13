using UnityEngine;

using Modul_21_1.Gameplay.Interface;

namespace Modul_21_1.Gameplay.Features
{
    public class ExplosionCaster : IClickButton
    {
        private LayerMask _interactionMask;
        private LayerMask _floorMask;
        private ParticleSystem _explosionEffect;

        private float _explosionRadius = 3f;
        private float _explosionPowerMultiplier = 300f;

        public ExplosionCaster(LayerMask interactionMask, LayerMask floorMask, ParticleSystem explosionEffect)
        {
            _interactionMask = interactionMask;
            _floorMask = floorMask;
            _explosionEffect = explosionEffect;
        }

        public void OnButtonDown()
        {
            Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (TryGetPointOnFloor(rayFromCamera, out Vector3 pointOnFloor))
            {
                ParticleSystem effect = Object.Instantiate(_explosionEffect, pointOnFloor, _explosionEffect.transform.rotation);

                Collider[] targets = Physics.OverlapSphere(pointOnFloor, _explosionRadius, _interactionMask);

                if (targets.Length == 0)
                    return;

                AddExplosionForceTo(pointOnFloor, targets);
            }
        }

        private void AddExplosionForceTo(Vector3 explosionPosition, Collider[] targets)
        {
            foreach (Collider target in targets)
            {
                Rigidbody rigidbody = target.GetComponent<Rigidbody>();

                if (rigidbody == null)
                    continue;

                Vector3 explosionVector = target.transform.position - explosionPosition;
                Vector3 explosionPower = explosionVector * (_explosionPowerMultiplier / explosionVector.magnitude);

                rigidbody.AddForce(explosionPower);
            }
        }
        private bool TryGetPointOnFloor(Ray mousePosition, out Vector3 dragPosition)
        {
            dragPosition = Vector3.zero;

            if (Physics.Raycast(mousePosition, out RaycastHit hit, Mathf.Infinity, _floorMask))
            {
                dragPosition = hit.point;
                return true;
            }

            return false;
        }
    }
}