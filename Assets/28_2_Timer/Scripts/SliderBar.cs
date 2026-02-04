using UnityEngine;
using UnityEngine.UI;

namespace Modul_28_2
{
    [RequireComponent(typeof(Slider))]
    public class SliderBar : Bar
    {
        private Slider _slider;

        public override void UpdateValue(int value)
        {
            _slider.value = value;
        }

        protected override void InitBar()
        {
            _slider = GetComponent<Slider>();
            _slider.maxValue = _maxValue;
            _slider.value = _slider.maxValue;
        }
    }
}