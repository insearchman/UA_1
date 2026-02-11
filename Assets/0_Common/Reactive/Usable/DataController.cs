using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modul_0
{
    public enum DataType
    {
        First,
        Second,
        Third,
    }

    // Инспектор френдли (вместо дикта)
    [Serializable]
    public class DataController : MonoBehaviour
    {
        [SerializeField] private List<DataClass> _dataClasses = new();

        public float GetValueByType(DataType type) => _dataClasses.First(dataClass => dataClass.Equals(type)).Value;

        // Приватный дата класс внутри констроллера бывает достаточным.
        [Serializable]
        private class DataClass
        {
            [field: SerializeField] public DataType Type { get; private set; }
            [field: SerializeField] public int Id {  get; private set; }
            [field: SerializeField] public string Name { get; private set; }
            [field: SerializeField] public float Value { get; private set; }
        }
    }
}