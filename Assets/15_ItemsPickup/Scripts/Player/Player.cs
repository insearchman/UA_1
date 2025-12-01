using UnityEngine;

namespace Modul_15
{
    [RequireComponent(typeof(Mover))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _rightHand;
        [SerializeField] private ParticleSystem _itemEffect;

        private InputSystem _input;
        private CameraController _mainCamera;
        private Mover _mover;

        public float WalkingSpeed { get; private set; } = 1.5f;
        public float RunningSpeed { get; private set; } = 5f;
        public float RotationSpeed { get; private set; } = 500f;
        public int SpeedBooster { get; private set; } = 1;
        public int Health { get; private set; } = 100;

        private Item currentItem;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _input = GetComponent<InputSystem>();

            _mainCamera = new();
            _mainCamera.InitCamera(transform.position);

            _input.OnMove += MoveHandler;
            _input.OnUseItem += UseItemHandler;
        }

        private void MoveHandler(Vector3 normalizedinput)
        {
            if (_input.IsRunning)
            {
                _mover.MoveCharacter(normalizedinput, RunningSpeed * SpeedBooster);
            }
            else
            {
                _mover.MoveCharacter(normalizedinput, WalkingSpeed * SpeedBooster);
            }

            _mover.RotateCharacter(normalizedinput, RotationSpeed);

            _mainCamera.Foloww(transform.position);
        }

        private void UseItemHandler()
        {
            if (currentItem != null)
            {
                currentItem.Use(this);
                currentItem = null;

                ShowJumpEffect();
            }
            else 
                Debug.Log("No item");
        }

        public int SpeedBoosterIncrease(int boost)
        {
            SpeedBooster += boost;
            return SpeedBooster;
        }
        public int HealthIncrease(int value)
        {
            Health += value;
            return Health;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (currentItem != null)
                return;

            Item item = other.GetComponent<Item>();

            if (item != null)
            {
                currentItem = item;

                item.transform.SetParent(_rightHand, false);
                item.transform.localPosition = Vector3.zero;
            }
        }

        private void ShowJumpEffect()
        {
            ParticleSystem jumpEffect = Instantiate(_itemEffect, transform.position, Quaternion.identity);

            jumpEffect.Play();

            Destroy(jumpEffect.gameObject, jumpEffect.main.duration + jumpEffect.main.startLifetime.constantMax);
        }
    }
}