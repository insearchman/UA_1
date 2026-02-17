using System;
using UnityEngine;

namespace Modul_30_2
{
    public class Dragon : Unit
    {
        public int Wisdom { get; private set; }

        protected override void AdditionalInit(UnitProperty property)
        {
            if (property is DragonProperty correctProperty)
            {
                Wisdom = correctProperty.Wisdom;
            }
            else
            {
                throw new InvalidCastException("Wrong property for Dragon");
            }

            Debug.Log($"=== Spawned - Dragon: Health = {CurrentHealth}; Wisdom = {Wisdom};");
        }
    }
}