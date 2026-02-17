using System.Collections.Generic;
using UnityEngine;

namespace Modul_30_1_2
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Transform _uiGroup;
        [SerializeField] List<Bar> barPrefabs = new List<Bar>();

        public void Init(Timer timer)
        {
            foreach (Bar barPrefab in barPrefabs)
            {
                Bar newBar = Instantiate(barPrefab, _uiGroup);
                newBar.Init(timer);
            }
        }
    }
}