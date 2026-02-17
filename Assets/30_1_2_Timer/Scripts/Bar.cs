using UnityEngine;

namespace Modul_30_1_2
{
    public abstract class Bar : MonoBehaviour
    {
        protected int _maxValue;

        public void Init(Timer timer)
        {
            _maxValue = timer.TimerTime;
            timer.TimerSecondsChanged += UpdateValue;

            InitBar();
        }

        protected abstract void InitBar();
        public abstract void UpdateValue(int value);

    }
}