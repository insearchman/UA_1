using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> OnCoinCollect;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.transform.parent.GetComponent<Player>();

        if(player != null)
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        OnCoinCollect?.Invoke(this);

        Destroy(gameObject);
    }
}
