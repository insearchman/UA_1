using UnityEngine;

namespace Modul_15
{
    public class PotionHealth : Item
    {
        private const string DebugMessage = "- Health potion used. Cuurent Health = {0}";

        private const int HealthIncrease = 10;

        public override void Use(Player player)
        {
            int health = player.HealthIncrease(HealthIncrease);

            Debug.LogFormat(DebugMessage, health);

            Destroy(gameObject);
        }
    }
}