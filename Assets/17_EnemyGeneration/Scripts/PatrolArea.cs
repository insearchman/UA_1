using System.Collections.Generic;
using UnityEngine;

namespace Modul_17
{
    public class PatrolArea : MonoBehaviour
    {
        public List<Transform> Points { get; private set; } = new();

        private void Awake()
        {
            foreach (Transform point in gameObject.GetComponentInChildren<Transform>())
                Points.Add(point);
        }
    }
}