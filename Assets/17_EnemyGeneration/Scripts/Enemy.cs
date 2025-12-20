using UnityEngine;

namespace Modul_17
{
    [RequireComponent(typeof(Mover))]
    public class Enemy : MonoBehaviour, IMovable
    {
        [SerializeField] private ParticleSystem _deathParticle;
        private IBehaviour _idleAction;
        private IBehaviour _activeAction;

        [SerializeField] public float WalkingSpeed { get; private set; } = 2f;
        [SerializeField] public float RotationSpeed { get; private set; } = 500f;
        public Mover Mover { get; private set; }
        public Transform Self => transform;
        public Transform Target {  get; private set; }

        private void Awake()
        {
            Mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (Target == null)
            {
                _idleAction?.Update();
            }
            else
            {
                _activeAction?.Update();
            }
        }

        public void SetBehaviours(IBehaviour idle, IBehaviour active)
        {
            _idleAction = idle;
            _activeAction = active;
        }

        public void Die()
        {
            if (_deathParticle != null)
            {
                ParticleSystem effect = Instantiate(_deathParticle, transform.position, _deathParticle.transform.rotation);
                effect.transform.SetParent(null);
            }

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                Target = player.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                Target = null;
            }
        }
    }
}