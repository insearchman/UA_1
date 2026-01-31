using System.Collections;
using UnityEngine;

namespace Modul_25.Gameplay
{
    [RequireComponent(typeof(Collider), typeof(AudioSource))]
    public class Mine : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionEffect;

        [SerializeField] private float _explosionRadius = 4f;
        [SerializeField] private int _explosionDamage = 30;

        [SerializeField] private float _timeToActivate = 2f;
        private AudioSource _explosionSound;
        private MeshRenderer _mineMesh;
        private WaitUntil _waitUntilPlayerDetected;

        private bool _isActivated;

        private void Start()
        {
            _waitUntilPlayerDetected = new WaitUntil(() => _isActivated);
            _explosionSound = GetComponent<AudioSource>();
            _mineMesh = GetComponentInChildren<MeshRenderer>();

            StartCoroutine(ActivateMine());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamagable>(out _))
            {
                _isActivated = true;
            }
        }

        private IEnumerator ActivateMine()
        {
            yield return _waitUntilPlayerDetected;

            float progress = 0;

            while (progress < _timeToActivate)
            {
                progress += Time.deltaTime;
                yield return null;
            }

            Explosion();
        }

        private void Explosion()
        {
            _explosionSound.Play();
            _mineMesh.enabled = false;

            Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            
            Collider[] targets = Physics.OverlapSphere(transform.position, _explosionRadius);

            foreach (Collider target in targets)
                if (target.TryGetComponent(out IDamagable confirmedTarget))
                    confirmedTarget.TakeDamage(_explosionDamage);

            Destroy(gameObject, _explosionSound.clip.length);
        }
    }
}