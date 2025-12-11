using UnityEngine;

namespace Modul_17
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private PatrolArea _area;

        public PatrolArea GetPatrolArea() => _area;
    }
}