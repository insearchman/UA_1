using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private List<Transform> _coinSpawnerPoint;

    private List<Coin> _activeCoins;

    public event Action OnAllCointsCollected;

    private void Awake()
    {
        _activeCoins = new();
    }

    public void GenerateCoins()
    {
        for (int i = 0; i < _coinSpawnerPoint.Count; i++)
        {
            Coin current = Instantiate(_coinPrefab, _coinSpawnerPoint[i]);

            current.OnCoinCollect += CollectCoin;
            _activeCoins.Add(current);
        }
    }


    public void CollectCoin(Coin coin)
    {
        _activeCoins.Remove(coin);

        if (_activeCoins.Count == 0)
        {
            OnAllCointsCollected?.Invoke();
        }
    }
}