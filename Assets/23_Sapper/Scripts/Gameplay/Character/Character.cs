using UnityEngine;

namespace Modul_23.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour, IMovable, IDamagable, ICharacterAnimatable
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _hurtedHealth = 30;

        [SerializeField] private Animator _characterAnimator;

        private CharacterHealth _health;
        private CharacterMover _mover;
        private CharacterView _view;

        [field: SerializeField] public float MoveSpeed { get; private set; } = 2.5f;
        [field: SerializeField] public float RotateSpeed { get; private set; } = 900f;
        public bool IsHurted { get; private set; }
        public bool IsAlive { get; private set; }
        public bool IsMoved { get; private set; }
        public int Hits { get; private set; }

        public CharacterController CharacterController { get; private set; }
        public Transform CurrentTransform => transform;
        public Vector3 CurrentVelocity => _mover.CurrentVelocity;

        private void Awake()
        {
            CharacterController = GetComponent<CharacterController>();

            _health = new CharacterHealth(_maxHealth);
            _mover = new CharacterMover(this);
            _view = new CharacterView(this, _characterAnimator);

            IsAlive = true;
        }

        private void Update()
        {
            _mover.Update(Time.deltaTime);
            IsMoved = _mover.IsMoved;

            _view.Update();
        }

        public void SetMoveDirection(Vector3 movePoint)
        {
            _mover.SetPointToMove(movePoint);
        }

        public void TakeDamage(int damage)
        {
            Hits++;
            _health.TakeDamage(damage);

            if (_health.CurrentHealth <= 0)
                Die();
            else if (_health.CurrentHealth <= _hurtedHealth)
                Hurted(true);
            else
                Hurted(false);
        }

        private void Die()
        {
            IsAlive = false;
        }

        private void Hurted(bool isHarted)
        {
            IsHurted = isHarted;
        }
    }
}