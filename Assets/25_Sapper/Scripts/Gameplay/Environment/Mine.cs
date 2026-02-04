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

        private Coroutine _explosionCoroutine;

        private void Start()
        {
            _explosionSound = GetComponent<AudioSource>();
            _mineMesh = GetComponentInChildren<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_explosionCoroutine == null)
                if (other.TryGetComponent<IDamagable>(out _))
                    _explosionCoroutine = StartCoroutine(ActivateMine());
        }

        private IEnumerator ActivateMine()
        {
            yield return new WaitForSeconds(_timeToActivate);

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

        private void OnDestroy()
        {
            StopCoroutine(_explosionCoroutine);
        }
    }
}