using System;

namespace Modul_0
{
    public interface IReadOnlyRValue<T>
    {
        public event Action<T> Changed;
        public event Action<T, T> ChangedWIthOld;

        T Value { get; }
    }
}