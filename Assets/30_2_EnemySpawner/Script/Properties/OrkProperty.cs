using System;
using UnityEngine;

namespace Modul_30_2
{
    [Serializable]
    public class OrkProperty : UnitProperty
    {
        [field: SerializeField, Range(0, 10)] public int Power;
    }
}