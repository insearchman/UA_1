using UnityEngine;

namespace Modul_17
{
    public interface IMovable
    {
        public float WalkingSpeed { get; }
        public float RotationSpeed { get; }
        public Transform Transform { get; }
        public Mover Mover { get; }
        public Player Target { get; }
        void Die();
    }
}