using UnityEngine;

namespace Modul_25.Gameplay
{
    public interface ICharacterAnimatable
    {
        bool IsHurted { get; }
        bool IsAlive { get; }
        int Hits { get; }
        Vector3 CurrentVelocity { get; }
    }
}