using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private List<Transform> _coinSpawnerPoint;

    public List<Coin> AllCoins {  get; private set; }

    private void Awake()
    {
        AllCoins = new();
    }

    public void GenerateCoins()
    {
        for (int i = 0; i < _coinSpawnerPoint.Count; i++)
        {
            Coin current = Instantiate(_coinPrefab, _coinSpawnerPoint[i]);
            AllCoins.Add(current);
        }
    }
}