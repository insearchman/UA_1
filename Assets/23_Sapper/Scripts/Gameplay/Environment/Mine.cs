using UnityEngine;

namespace Modul_23.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class Mine : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionEffect;

        [SerializeField] private float _explosionRadius = 4f;
        [SerializeField] private int _explosionDamage = 30;

        [SerializeField] private float _timeToActivate = 2f;

        private bool _isActivated;

        private void Update()
        {
            if (_isActivated == false)
                return;

            _timeToActivate -= Time.deltaTime;

            if (_timeToActivate < 0)
            {
                _isActivated = false;
                Explosion();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamagable>(out _))
            {
                _isActivated = true;
            }
        }

        private void Explosion()
        {
            Instantiate(_explosionEffect, transform.position, Quaternion.identity);

            Collider[] targets = Physics.OverlapSphere(transform.position, _explosionRadius);

            foreach (Collider target in targets)
                if (target.TryGetComponent(out IDamagable confirmedTarget))
                    confirmedTarget.TakeDamage(_explosionDamage);

            Destroy(gameObject);
        }
    }
}