using UnityEngine;

namespace Modul_23.Gameplay
{
    public class CharacterView
    {
        private const float MinWalkingSpeed = 0.05f;

        private readonly int IsWalkingBool = Animator.StringToHash("IsWalking");
        private readonly int HitTrigger = Animator.StringToHash("Hit");
        private readonly int IsDeadBool = Animator.StringToHash("IsDead");

        private readonly ICharacterAnimatable Character;
        private readonly Animator Animator;

        private readonly int NormalLayerIndex;
        private readonly int HartedLayerIndex;

        private bool _isHarted;
        private int _hits;

        public CharacterView(ICharacterAnimatable character, Animator animator)
        {
            Character = character;
            Animator = animator;

            NormalLayerIndex = Animator.GetLayerIndex("Normal");
            HartedLayerIndex = Animator.GetLayerIndex("Harted");
        }

        public void Update()
        {
            CheckAlive();
            SwitchStateLayer();
            SwitchWalkingAnimation();
            CheckHit();
        }

        private void SwitchStateLayer()
        {
            if (_isHarted != Character.IsHurted)
            {
                _isHarted = Character.IsHurted;

                if (_isHarted)
                    ActivateLayer(HartedLayerIndex);
                else
                    ActivateLayer(NormalLayerIndex);
            }
        }

        private void ActivateLayer(int layer)
        {
            for (int i = 1; i < Animator.layerCount; i++)
            {
                Animator.SetLayerWeight(i, 0f);
            }

            if (layer > 0)
            {
                Animator.SetLayerWeight(layer, 1f);
            }
        }

        private void SwitchWalkingAnimation()
        {
            if (Character.CurrentVelocity.magnitude > MinWalkingSpeed)
                Animator.SetBool(IsWalkingBool, true);
            else
                Animator.SetBool(IsWalkingBool, false);
        }

        private void CheckAlive()
        {
            if (Character.IsAlive == false)
                Animator.SetBool(IsDeadBool, true);
        }

        private void CheckHit()
        {
            if (Character.Hits > _hits)
            {
                _hits = Character.Hits;
                Animator.SetTrigger(HitTrigger);
            }
        }
    }
}