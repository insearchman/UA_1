using UnityEngine;

namespace Modul_0
{
    public class Health
    {
        private RValue<float> _max;
        private RValue<float> _current;

        public Health(float max, float current)
        {
            _max.Value = max;
            _current.Value = current;
        }

        public IReadOnlyRValue<float> Max => _max;
        public IReadOnlyRValue<float> Current => _current;

        public void Add(int value)
        {
            if (value < 0)
                return;

            _current.Value = Mathf.Clamp(_current.Value + value, 0, _max.Value);
        }

        public void Reduce(int value)
        {
            if (value < 0)
                return;

            _current.Value = Mathf.Clamp(_current.Value - value, 0, _max.Value);
        }
    }
}