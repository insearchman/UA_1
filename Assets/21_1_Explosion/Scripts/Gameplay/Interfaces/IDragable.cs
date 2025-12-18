using UnityEngine;

namespace Modul_21_1.Gameplay
{
    public interface IDragable
    {
        void OnGrab();
        void OnDrag(Vector3 position);
        void OnRelease();
    }
}