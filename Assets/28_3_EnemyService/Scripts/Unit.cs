using UnityEngine;

namespace Modul_28_3
{
    public class Unit : MonoBehaviour
    {
        public bool IsDead {  get; private set; }

        public void Kill() => IsDead = true;
    }
}