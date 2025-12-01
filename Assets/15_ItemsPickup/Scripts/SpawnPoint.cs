using UnityEngine;

namespace Modul_15
{
    public class SpawnPoint : MonoBehaviour
    {
        public bool IsEmpty => transform.childCount == 0;

        public Vector3 Position => transform.position;
    }
}