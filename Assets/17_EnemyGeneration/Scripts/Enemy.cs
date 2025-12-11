using UnityEngine;

namespace Modul_17
{
    [RequireComponent(typeof(Mover))]
    public class Enemy : MonoBehaviour
    {
        public Player Player {  get; private set; }
        public PatrolArea Area { get; private set; }
        public Mover Mover { get; private set; }
        public float WalkingSpeed { get; private set; } = 2f;
        public float RotationSpeed { get; private set; } = 500f;

        [SerializeField] private ParticleSystem _deathParticle;

        private BehaviourController _behaviourController;
        private IBehaviour _idleAction;
        private IBehaviour _activeAction;

        private void Awake()
        {
            _behaviourController = new BehaviourController();
            Mover = GetComponent<Mover>();

            Area = FindAnyObjectByType<PatrolArea>();
        }

        private void Update()
        {
            if (Player == null)
            {
                _idleAction.Update();
            }
            else
            {
                _activeAction.Update();
            }
        }

        public void SetBehaviours(IdleBehaviourTypes idle, ActiveBehaviourTypes action)
        {
            _idleAction = _behaviourController.GetIdleBehaviour(idle, this);
            _activeAction = _behaviourController.GetActiveBehaviour(action, this);
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
                Player = player;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                Player = null;
            }
        }
    }
}