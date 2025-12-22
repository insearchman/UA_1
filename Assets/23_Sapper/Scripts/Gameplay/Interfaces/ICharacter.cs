using UnityEngine;

namespace Modul_23.Gameplay
{
    public interface ICharacter
    {
        bool IsHurted { get; }
        bool IsAlive { get; }
        int Hits { get; }
        Vector3 CurrentVelocity { get; }
        Vector3 MovePosition { get; }
    }
}