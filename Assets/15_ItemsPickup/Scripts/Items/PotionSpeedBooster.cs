using UnityEngine;

namespace Modul_15
{
    public class PotionSpeedBooster : Item
    {
        private const string DebugMessage = "- SpeedBooster potion used. Cuurent SpeedBooster = {0}";

        private const int SpeedBooster = 1;

        public override void Use(Player player)
        {
            int speedBooster = player.SpeedBoosterIncrease(SpeedBooster);

            Debug.LogFormat(DebugMessage, speedBooster);

            Destroy(gameObject);
        }
    }
}