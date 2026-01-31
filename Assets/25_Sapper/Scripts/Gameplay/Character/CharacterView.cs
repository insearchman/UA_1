using System;
using UnityEngine;

namespace Modul_25.Gameplay
{
    public class CharacterView
    {
        private const float MinWalkingSpeed = 0.06f;

        private readonly int IsWalkingBool = Animator.StringToHash("IsWalking");
        private readonly int HitTrigger = Animator.StringToHash("Hit");
        private readonly int IsDeadBool = Animator.StringToHash("IsDead");
        private readonly int JumpTrigger = Animator.StringToHash("Jump");

        private ICharacterAnimatable _character;
        private Animator _animator;

        private readonly int NormalLayerIndex;
        private readonly int HartedLayerIndex;

        private const string JUMP_ANIMATION_NAME = "Jump";
        private const string JUMP_ANIMATION_SPEED = "JumpSpeed";
        private AnimationClip _jumpClip;

        private bool _isHarted;
        private int _hits;
        private bool _inJump;

        public CharacterView(ICharacterAnimatable character, Animator animator)
        {
            _character = character;
            _animator = animator;

            _jumpClip = GetClipByName(JUMP_ANIMATION_NAME);

            NormalLayerIndex = _animator.GetLayerIndex("Normal");
            HartedLayerIndex = _animator.GetLayerIndex("Harted");
        }

        public void Update()
        {
            CheckAlive();

            if (IsAgentCharacter(out AgentCharacter agent))
            {
                CheckJump(agent);
            }
            
            SwitchStateLayer();
            SwitchWalkingAnimation();
            CheckHit();
        }

        private bool IsAgentCharacter(out AgentCharacter agent)
        {
            if (_character is AgentCharacter agentCharacter)
            {
                agent = agentCharacter;
                return true;
            }

            agent = null;
            return false;
        }

        private void CheckJump(AgentCharacter agent)
        {
            if (_inJump == false & agent.InJumpProcess)
            {
                float animationSpeed = _jumpClip.length / agent.JumpDuration;
                _animator.SetFloat(JUMP_ANIMATION_SPEED, animationSpeed);
                _animator.SetTrigger(JumpTrigger);
            }

            _inJump = agent.InJumpProcess;
        }

        private void SwitchStateLayer()
        {
            if (_isHarted != _character.IsHurted)
            {
                _isHarted = _character.IsHurted;

                if (_isHarted)
                    ActivateLayer(HartedLayerIndex);
                else
                    ActivateLayer(NormalLayerIndex);
            }
        }

        private void ActivateLayer(int layer)
        {
            for (int i = 1; i < _animator.layerCount; i++)
                _animator.SetLayerWeight(i, 0f);

            if (layer > 0)
                _animator.SetLayerWeight(layer, 1f);
        }

        private void SwitchWalkingAnimation()
        {
            if (_character.CurrentVelocity.magnitude > MinWalkingSpeed)
                _animator.SetBool(IsWalkingBool, true);
            else
                _animator.SetBool(IsWalkingBool, false);
        }

        private void CheckAlive()
        {
            if (_character.IsAlive == false)
                _animator.SetBool(IsDeadBool, true);
        }

        private void CheckHit()
        {
            if (_character.Hits > _hits)
            {
                _hits = _character.Hits;
                _animator.SetTrigger(HitTrigger);
            }
        }
        private AnimationClip GetClipByName(string clipName)
        {
            foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == clipName)
                    return clip;
            }
            return null;
        }
    }
}