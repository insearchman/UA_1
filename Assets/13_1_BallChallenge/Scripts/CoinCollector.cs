using System;
using UnityEngine;

namespace Modul_13_1
{
    public class CoinCollector : MonoBehaviour
    {
        public event Action OnCointCollect;

        private void OnTriggerEnter(Collider other)
        {
            Coin coin = other.transform.GetComponent<Coin>();

            if (coin != null)
            {
                coin.CollectCoin();
                OnCointCollect?.Invoke();
            }
        }
    }
}