using UnityEngine;

namespace Modul_23.Gameplay
{
    public class CharacterHealth
    {
        private readonly int MaxHealth;
        public int CurrentHealth {  get; private set; }

        public CharacterHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage == 0)
                return;

            if (damage < 0)
            {
                Debug.LogError("Error: Damage can't be less then 0");
                return;
            }

            CurrentHealth -= damage;

            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
        }
    }
}