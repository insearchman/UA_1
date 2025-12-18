using UnityEngine;

namespace Modul_23.Gameplay
{
    public interface IMovable
    {
        float MoveSpeed { get; }
        float RotateSpeed { get; }
        CharacterController CharacterController { get; }
        Transform CurrentTransform { get; }
    }
}