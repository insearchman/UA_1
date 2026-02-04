using UnityEngine;
using UnityEngine.UI;

namespace Modul_28_2
{
    public class ElementBar : Bar
    {
        [SerializeField] private Image _pointPrefab;
        [SerializeField] private Sprite _filledPrefab;
        [SerializeField] private Sprite _emptyPrefab;

        private Image[] _barValues;

        public override void UpdateValue(int value)
        {
            for (int i = _barValues.Length-1; i >= value; i--)
            {
                if (_barValues[i].sprite != _emptyPrefab)
                    _barValues[i].sprite = _emptyPrefab;
            }
        }

        protected override void InitBar()
        {
            _barValues = new Image[_maxValue];

            for (int i = 0; i < _maxValue; i++)
            {
                _barValues[i] = Instantiate(_pointPrefab, transform);
                _barValues[i].sprite = _filledPrefab;
            }
        }
    }
}