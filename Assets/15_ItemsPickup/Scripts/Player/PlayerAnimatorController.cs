using UnityEngine;

namespace Modul_15
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        private const string WalkAnimationTrigger = "isWalking";
        private const string RunAnimationTrigger = "isRunning";

        private InputSystem _input;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _input = GetComponent<InputSystem>();

            _input.OnMove += HandlerMove;
            _input.OnStopMove += HandlerStopMove;
        }

        private void HandlerStopMove()
        {
            WalkAnimation(false);
            RunningAnimation(false);
        }

        private void HandlerMove(Vector3 vector)
        {
            WalkAnimation(true);
            RunningAnimation(_input.IsRunning);
        }

        private void WalkAnimation(bool isWalking)
        {
            _animator.SetBool(WalkAnimationTrigger, isWalking);
        }

        private void RunningAnimation(bool isRunning)
        {
            _animator.SetBool(RunAnimationTrigger, isRunning);
        }
    }
}