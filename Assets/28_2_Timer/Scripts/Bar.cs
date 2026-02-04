using UnityEngine;

namespace Modul_28_2
{
    public abstract class Bar : MonoBehaviour
    {
        protected int _maxValue;

        public void Init(int maxValue)
        {
            _maxValue = maxValue;

            InitBar();
        }

        protected abstract void InitBar();

        public abstract void UpdateValue(int value);
    }
}