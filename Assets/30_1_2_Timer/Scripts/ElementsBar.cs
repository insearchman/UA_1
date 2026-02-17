using UnityEngine;

namespace Modul_30_1_2
{
    public class ElementsBar : Bar
    {
        [SerializeField] private BarElement _elementPrefab;

        private BarElement[] _barValues;

        protected override void InitBar()
        {
            _barValues = new BarElement[_maxValue];

            for (int i = 0; i < _maxValue; i++)
                _barValues[i] = Instantiate(_elementPrefab, transform);
        }

        public override void UpdateValue(int value)
        {
            for (int i = _barValues.Length-1; i >= value; i--)
                if (!_barValues[i].IsFilled)
                    _barValues[i].SwitchSprite(false);
        }
    }
}