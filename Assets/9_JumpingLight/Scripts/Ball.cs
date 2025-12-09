using UnityEngine;

namespace Modul_9
{
    public enum JumpDirection
    {
        Up,
        Left,
        Right
    }

    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private GameObject _jumpEffectPrefab;

        [SerializeField] private int _jumpMinImpulse = 4;
        [SerializeField] private int _jumpMaxImpulse = 6;
        [SerializeField] private int _jumpSideMinImpulse = 4;
        [SerializeField] private int _jumpSideMaxImpulse = 6;

        private Rigidbody _rigidBody;
        private Animator _animator;

        public int UpJumpCount { get; private set; }
        public int SideJumpCount { get; private set; }

        public bool IsKinematic { get => _rigidBody.isKinematic; }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        public void Jump(JumpDirection direction)
        {
            switch (direction)
            {
                case JumpDirection.Up:
                    _rigidBody.AddForce(GetRandomForce(), ForceMode.Impulse);
                    UpJumpCount++;
                    break;
                case JumpDirection.Left:
                    _rigidBody.AddForce(GetRandomSideForce(JumpDirection.Left), ForceMode.Impulse);
                    SideJumpCount++;
                    break;
                case JumpDirection.Right:
                    _rigidBody.AddForce(GetRandomSideForce(JumpDirection.Right), ForceMode.Impulse);
                    SideJumpCount++;
                    break;
                default:
                    Debug.Log("Error: Wrong jump direction");
                    break;
            }

            _animator.SetTrigger("Jump");

            ShowJumpEffect();
        }

        private Vector3 GetRandomForce()
        {
            return new Vector3(0, Random.Range(_jumpMinImpulse, _jumpMaxImpulse), 0);
        }

        private Vector3 GetRandomSideForce(JumpDirection direction)
        {
            Vector3 jumpForce = Vector3.zero;

            if (direction == JumpDirection.Left)
                jumpForce = new Vector3(-Random.Range(_jumpSideMinImpulse, _jumpSideMaxImpulse), Random.Range(_jumpMinImpulse, _jumpMaxImpulse), 0);
            else if (direction == JumpDirection.Right)
                jumpForce = new Vector3(Random.Range(_jumpSideMinImpulse, _jumpSideMaxImpulse), Random.Range(_jumpMinImpulse, _jumpMaxImpulse), 0);

            return jumpForce;
        }

        public void ResetBallState()
        {
            ResetJumpCouters();

            SetKinematic(true);

            gameObject.SetActive(true);

            transform.position = Vector3.zero;

            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        private void ResetJumpCouters()
        {
            UpJumpCount = 0;
            SideJumpCount = 0;
        }

        public void SetKinematic(bool kinematic)
        {
            _rigidBody.isKinematic = kinematic;
        }

        private void ShowJumpEffect()
        {
            Vector3 jumpEffectPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.05f);

            GameObject jumpEffect = Instantiate(_jumpEffectPrefab, jumpEffectPosition, Quaternion.identity);
            ParticleSystem jumpEffectPS = jumpEffect.GetComponent<ParticleSystem>();

            jumpEffectPS.Play();

            Destroy(jumpEffect, jumpEffectPS.main.duration + jumpEffectPS.main.startLifetime.constantMax);
        }
    }
}