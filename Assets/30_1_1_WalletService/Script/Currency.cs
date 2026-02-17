using System;
using UnityEngine;

namespace Modul_30_1_1
{
    public class Currency
    {
        public event Action<CurrencyType, int> ValueChanged;

        private const int MINIMUM_VALUE = 1;
        private const int EMPTY_VALUE = 0;
        private const int DEFAULT_CAPACITY = 100;

        private int _value;

        public Currency(CurrencyType currencyType, int capacity = DEFAULT_CAPACITY)
        {
            CurrencyType = currencyType;
            Capacity = capacity;
        }

        public CurrencyType CurrencyType { get; private set; }
        public int Capacity {  get; private set; }

        public int Value
        {
            get => _value;
            private set
            {
                if (_value != value)
                {
                    _value = value;
                    ValueChanged?.Invoke(CurrencyType, _value);
                }
            }
        }

        public void Add(int value = MINIMUM_VALUE)
        {
            Value += value;

            if (Value > Capacity)
                Value = Capacity;
        }

        public void Spend(int value = MINIMUM_VALUE)
        {
            if (IsEnough(value))
                Value -= value;
            else
                Debug.Log($"Not enough {CurrencyType}");
        }

        public bool IsEnough(int value) 
            => Value - value >= EMPTY_VALUE;

        public void Clear()
        {
            Value = EMPTY_VALUE;
        }
    }
}