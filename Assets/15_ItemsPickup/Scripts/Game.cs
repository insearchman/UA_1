using System.Collections.Generic;
using UnityEngine;

namespace Modul_15
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints;
        [SerializeField] private List<Item> _itemPrefabs;
        [SerializeField] private float _cooldownItemSpawn = 5;

        private bool _isEmptySpawnExist;

        private float _time;

        private void Start()
        {
            ItemRandomSpawn();
        }

        private void Update()
        {
            if (_isEmptySpawnExist)
            {
                _time += Time.deltaTime;

                if (_time > _cooldownItemSpawn)
                {
                    ItemRandomSpawn();

                    _time = 0;

                    _isEmptySpawnExist = CheckSpawnPoints();
                }
            }
            else
            {
                _isEmptySpawnExist = CheckSpawnPoints();
            }
        }

        private void ItemRandomSpawn()
        {
            foreach (SpawnPoint point in _spawnPoints)
            {
                if (point.IsEmpty)
                {
                    Item item = Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Count)], point.Position, Quaternion.identity);
                    item.transform.SetParent(point.transform);
                }
            }
        }

        private bool CheckSpawnPoints()
        {
            foreach (SpawnPoint point in _spawnPoints)
                if (point.IsEmpty)
                    return true;

            return false;
        }
    }
}