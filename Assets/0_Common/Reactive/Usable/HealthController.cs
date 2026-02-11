
using UnityEngine;

namespace Modul_0 
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private SliderView _healthView;
        private Health _health;

        private void Awake()
        {
            _health = new Health(100, 100);
            _healthView.Init(_health.Current, _health.Max);
            _health.Current.Changed += OnHealthCurrentCanged;
        }



        private void OnHealthCurrentCanged(float value)
        {
            if (value < 0)
                Debug.Log("Dead man here");
        }

        private void OnDestroy()
        {
            _health.Current.Changed -= OnHealthCurrentCanged;
        }
    }
}