using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Modul_25.Gameplay
{
    public class Health
    {
        private readonly int MaxHealth;
        public int CurrentHealth {  get; private set; }

        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int value)
        {
            if (!IsCorrectValue(value))
                return;

            CurrentHealth -= value;

            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }

        public void Heal(int value)
        {
            if (!IsCorrectValue(value))
                return;

            CurrentHealth += value;

            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }

        private bool IsCorrectValue(int value)
        {
            if (value == 0)
                return false;

            if (value < 0)
            {
                Debug.LogError("Error: Value can't be less then 0");
                return false;
            }

            return true;
        }
    }
}