using UnityEngine;

namespace Modul_25.Gameplay
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Character : MonoBehaviour, IDamagable, ICharacterAnimatable
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _hurtedHealth = 30;

        [SerializeField] private Animator _characterAnimator;

        protected Mover _mover;
        private Health _health;
        private CharacterView _view;

        protected CharacterAudio _audio;
        private AudioSource _stepSource;

        [field: SerializeField] public float MoveSpeed { get; private set; } = 2.5f;
        [field: SerializeField] public float JumpSpeed { get; private set; } = 2.5f;
        [field: SerializeField] public float RotateSpeed { get; private set; } = 900f;

        [SerializeField] private AudioClip[] _footstepClips;
        [SerializeField] private float _stepDelay = 0.5f;

        public bool IsHurted { get; private set; }
        public bool IsAlive { get; private set; }
        public bool IsMoved { get; private set; }
        public int Hits { get; private set; }

        public Transform CurrentTransform => transform;
        public Vector3 CurrentVelocity => _mover.CurrentVelocity;

        protected virtual void Awake()
        {
            _health = new Health(_maxHealth);
            _view = new CharacterView(this, _characterAnimator);

            _stepSource = GetComponent<AudioSource>();
            _audio = new CharacterAudio(_stepSource, _footstepClips, _stepDelay);

            IsAlive = true;
        }

        private void Update()
        {
            _view.Update();

            if (IsAlive == false)
                return;

            UpdateMover();
            IsMoved = _mover.IsMoved;

            if (IsMoved)
                PlayMoveSound();
        }

        protected virtual void UpdateMover()
        {
            _mover.Update(Time.deltaTime);
        }

        private void PlayMoveSound()
        {
            _audio.PlayRandomFootstep();
        }

        public virtual void SetMoveDirection(Vector3 moveDirection)
        {
            _mover.SetMoveDirection(moveDirection);
        }

        public void TakeDamage(int damage)
        {
            Hits++;
            _health.TakeDamage(damage);

            if (_health.CurrentHealth <= 0)
                Die();
            else
                SetHurted();
        }

        public void Heal(int value)
        {
            _health.Heal(value);

            SetHurted();
        }

        private void Die()
        {
            IsAlive = false;
        }

        private void SetHurted()
        {
            if (_health.CurrentHealth <= _hurtedHealth)
                IsHurted = true;
            else
                IsHurted = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Item item))
            {
                item.Use(this);
            }
        }
    }
}