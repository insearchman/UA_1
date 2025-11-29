using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

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