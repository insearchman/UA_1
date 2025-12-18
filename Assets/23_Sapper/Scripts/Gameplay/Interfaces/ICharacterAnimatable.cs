using UnityEngine;

namespace Modul_23.Gameplay
{
    public interface ICharacterAnimatable
    {
        bool IsHurted { get; }
        bool IsAlive { get; }
        int Hits { get; }
        Vector3 CurrentVelocity { get; }
    }
}