using UnityEngine;

namespace Modul_13_1
{
    public class Coin : MonoBehaviour
    {
        public void CollectCoin()
        {
            Destroy(gameObject);
        }
    }
}