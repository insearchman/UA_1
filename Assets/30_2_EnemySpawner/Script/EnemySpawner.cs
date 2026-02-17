using System;
using UnityEngine;

namespace Modul_30_2
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Ork _orkPrefab;
        [SerializeField] private Elf _elfPrefab;
        [SerializeField] private Dragon _dragonPrefab;

        public void Spawn(UnitProperty property, Vector3 position)
        {
            Unit newUnit = null;

            if (property is OrkProperty)
                newUnit = Instantiate(_orkPrefab, position, Quaternion.identity);

            if (property is ElfProperty)
                newUnit = Instantiate(_elfPrefab, position, Quaternion.identity);

            if (property is DragonProperty)
                newUnit = Instantiate(_dragonPrefab, position, Quaternion.identity);

            if (newUnit)
                newUnit.Init(property);
            else
                throw new ArgumentOutOfRangeException(nameof(property), "Unknown type of property for spawn");

        }
    }
}