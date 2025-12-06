using System;
using UnityEngine;

namespace Modul_17
{
    public abstract class InputSystem : MonoBehaviour
    {
        public abstract event Action<Vector3> OnMove;
        public abstract event Action OnStopMove;
        public abstract event Action OnUseItem;

        public bool IsRunning { get; protected set; }
    }
}