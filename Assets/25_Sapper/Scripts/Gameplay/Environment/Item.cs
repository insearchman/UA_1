using UnityEngine;

namespace Modul_25.Gameplay
{
    public class Item : MonoBehaviour
    {
        private int _healthHealing;

        public void Init(int healthHealing)
        {
            _healthHealing = healthHealing;
        }

        public void Use(Character character)
        {
            character.Heal(_healthHealing);
            
            Destroy(gameObject);
        }
    }
}