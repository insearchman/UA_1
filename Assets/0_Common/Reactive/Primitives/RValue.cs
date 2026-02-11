using System;

namespace Modul_0
{
    public class RValue<T> : IReadOnlyRValue<T> where T : IEquatable<T>
    {
        public event Action<T> Changed;
        public event Action<T, T> ChangedWIthOld;

        private T _value;

        // «ащиты от записи нет, так как тут нет методов дл€ изменени€
        // ”правленние валидацией происходит в используемом класе с помощью IReadOnlyRValue
        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;

                _value = value;

                if (oldValue.Equals(_value))
                {
                    Changed?.Invoke(_value);
                    ChangedWIthOld?.Invoke(oldValue, _value);
                }
            }
        }
    }
}