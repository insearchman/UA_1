using System;
using UnityEngine;

namespace Modul_30_1_2
{
    public class Rfloat
    {
        public event Action<int> ChangedIntPart;

        private float _value;

        public float Value
        {
            get => _value;
            set
            {
                int oldCeilValue = Mathf.CeilToInt(_value);
                int newCeilValue = Mathf.CeilToInt(value);

                if (oldCeilValue != newCeilValue)
                {
                    ChangedIntPart?.Invoke(newCeilValue);
                }

                _value = value;
            }
        }
    }
}