using UnityEngine;
using UnityEngine.UI;

namespace Modul_0
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private IReadOnlyRValue<float> _max;
        private IReadOnlyRValue<float> _current;

        public void Init(IReadOnlyRValue<float> current, IReadOnlyRValue<float> max)
        {
            _current = current;
            _max = max;

            _current.Changed += OnCurrentChanged;
            _max.Changed += OnMaxChanged;

            UpdateValues(_current.Value, _max.Value);
        }


        private void OnCurrentChanged(float value) => UpdateValues(_current.Value, value);

        private void OnMaxChanged(float value) => UpdateValues(value, _max.Value);

        private void UpdateValues(float current, float max) => _slider.value = current / max;

        private void OnDestroy()
        {
            _current.Changed -= OnCurrentChanged;
            _max.Changed -= OnMaxChanged;
        }
    }
}