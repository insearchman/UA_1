using System;
using UnityEngine;

namespace Modul_30_2
{
    [Serializable]
    public class DragonProperty : UnitProperty
    {
        [field: SerializeField, Range(0, 10)] public int Wisdom;
    }
}