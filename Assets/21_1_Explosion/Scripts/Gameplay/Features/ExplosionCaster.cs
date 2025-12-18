using UnityEngine;

namespace Modul_21_1.Gameplay
{
    public class ExplosionCaster : IClickButtonHandler
    {
        private readonly float ExplosionRadius;
        private readonly float ExplosionPowerMultiplier;

        private readonly LayerMask InteractionMask;
        private readonly LayerMask FloorMask;
        private readonly ParticleSystem ExplosionEffect;

        public ExplosionCaster(LayerMask interactionMask, LayerMask floorMask, ParticleSystem explosionEffect, float explosionRadius, float ExplosionPowerMultiplay)
        {
            InteractionMask = interactionMask;
            FloorMask = floorMask;
            ExplosionEffect = explosionEffect;
            ExplosionRadius = explosionRadius;
            ExplosionPowerMultiplier = ExplosionPowerMultiplay;
        }

        public void OnButtonDown()
        {
            Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (TryGetPointOnFloor(rayFromCamera, out Vector3 pointOnFloor))
            {
                Object.Instantiate(ExplosionEffect, pointOnFloor, ExplosionEffect.transform.rotation);

                Collider[] targets = Physics.OverlapSphere(pointOnFloor, ExplosionRadius, InteractionMask);
                
                if (targets.Length == 0)
                    return;

                AddExplosionForceTo(pointOnFloor, targets);
            }
        }

        private void AddExplosionForceTo(Vector3 explosionPosition, Collider[] targets)
        {
            foreach (Collider target in targets)
            {
                if (target.TryGetComponent(out IExplosionInteractable explosionInteractable) == false)
                    continue;

                Vector3 explosionVector = target.transform.position - explosionPosition;
                Vector3 explosionPower = explosionVector * (ExplosionPowerMultiplier / explosionVector.magnitude);

                explosionInteractable.ExplosionInteract(explosionPower);
            }
        }
        private bool TryGetPointOnFloor(Ray mousePosition, out Vector3 hitPosition)
        {
            hitPosition = Vector3.zero;

            if (Physics.Raycast(mousePosition, out RaycastHit hit, Mathf.Infinity, FloorMask))
            {
                hitPosition = hit.point;
                return true;
            }

            return false;
        }
    }
}