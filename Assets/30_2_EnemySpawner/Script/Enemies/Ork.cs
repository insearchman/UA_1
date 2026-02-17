using System;
using UnityEngine;

namespace Modul_30_2
{
    public class Ork : Unit
    {
        public int Power {  get; private set; }

        protected override void AdditionalInit(UnitProperty property)
        {
            if (property is OrkProperty correctProperty)
            {
                Power = correctProperty.Power;
            }
            else
            {
                throw new InvalidCastException("Wrong property for Ork");
            }

            Debug.Log($"=== Spawned - Ork: Health = {CurrentHealth}; Power = {Power};");
        }
    }
}