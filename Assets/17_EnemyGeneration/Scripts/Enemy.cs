using UnityEngine;

namespace Modul_17
{
    [RequireComponent(typeof(BehaviourController), typeof(Mover))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _deathParticle;

        private BehaviourController _behaviourController;
        private EnemyBehaviour _idleAction;
        private EnemyBehaviour _activeAction;

        private Player _player = null;
        private Mover _mover;

        private Vector3 currentDirectionToMove;

        public float WalkingSpeed { get; private set; } = 2f;
        public float RotationSpeed { get; private set; } = 500f;

        private void Awake()
        {
            _behaviourController = GetComponent<BehaviourController>();
            _mover = GetComponent<Mover>();
        }

        private void Start()
        {
            _idleAction = _behaviourController.IdleBehaviour;
            _activeAction = _behaviourController.ActiveBehaviour;
        }

        private void Update()
        {
            if (_player == null)
            {
                Action(_idleAction);
            }
            else
            {
                Action(_activeAction);
            }
        }

        private void Action(EnemyBehaviour action)
        {
            switch (action)
            {
                case ISimpleMovable simpleMovable:
                    currentDirectionToMove = simpleMovable.GetDirectionToMove();
                    _mover.MoveCharacter(currentDirectionToMove, WalkingSpeed, RotationSpeed);
                    break;
                case ITargetMovable targetMovable:
                    currentDirectionToMove = targetMovable.GetDirectionToMove(_player.gameObject);
                    _mover.MoveCharacter(currentDirectionToMove, WalkingSpeed, RotationSpeed);
                    break;
                case IEffectAction simpleAction:
                    simpleAction.Action(_deathParticle);
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                _player = player;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                _player = null;
            }
        }
    }
}