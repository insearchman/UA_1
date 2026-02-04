using System.Collections.Generic;
using UnityEngine;

namespace Modul_28_2
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private Transform _uiGroup;
        [SerializeField] List<Bar> barPrefabs = new List<Bar>();

        private int _maxValue;

        public void Init(Timer timer)
        {
            _maxValue = timer.TimerTime;

            foreach (Bar barPrefab in barPrefabs)
            {
                Bar newBar = Instantiate(barPrefab, _uiGroup);
                newBar.Init(_maxValue);
                timer.TimerSecondsChanged += newBar.UpdateValue;
            }
        }
    }
}