using UnityEngine;

namespace Modul_25.Gameplay
{
    public class KeyboardInput
    {
        private const KeyCode DEFAULT_HEAL_ACTIVATION_KEY = KeyCode.F;
        private KeyCode _healActiveKey;

        public bool IsHealActive { get; private set; } = true;

        public KeyboardInput(KeyCode healKey = DEFAULT_HEAL_ACTIVATION_KEY)
        {
            _healActiveKey = healKey;
        }

        public void Update()
        {
            HealActivateSwitch();
        }

        private void HealActivateSwitch()
        {
            if (Input.GetKeyDown(_healActiveKey))
            {
                IsHealActive = !IsHealActive;
            }
        }
    }
}