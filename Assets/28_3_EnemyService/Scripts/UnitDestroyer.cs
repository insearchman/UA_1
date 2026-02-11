using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modul_28_3
{
    public class UnitDestroyer : MonoBehaviour
    {
        [SerializeField] private int _unitsCapacity = 5;

        private List<(Unit unit, Func<bool> check)> _units = new();

        public void Update()
        {
            CheckUnits();
            Debug.Log(GetUnitsCount());
        }

        public bool IsOutOfCapacity() => _units.Count > _unitsCapacity;

        public int GetUnitsCount() => _units.Count;

        public void AddUnit(Unit unit, Func<bool> killType)
        {
            _units.Add((unit, killType));
        }

        private void CheckUnits()
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i].check())
                {
                    Destroy(_units[i].unit.gameObject);
                    _units.RemoveAt(i);
                }
            }
        }
    }
}