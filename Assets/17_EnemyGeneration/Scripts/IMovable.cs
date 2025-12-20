using UnityEngine;

namespace Modul_17
{
    public interface IMovable
    {
        public float WalkingSpeed { get; }
        public float RotationSpeed { get; }
        public Transform Target { get; }
        public Transform Self { get; }
        public Mover Mover { get; }
        void Die();
    }
}