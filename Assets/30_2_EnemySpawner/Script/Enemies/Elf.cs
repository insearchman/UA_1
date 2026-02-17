using System;
using UnityEngine;

namespace Modul_30_2
{
    public class Elf : Unit
    {
        public int Agility { get; private set; }

        protected override void AdditionalInit(UnitProperty property)
        {
            if (property is ElfProperty correctProperty)
            {
                Agility = correctProperty.Agility;
            }
            else
            {
                throw new InvalidCastException("Wrong property for Elf");
            }

            Debug.Log($"=== Spawned - Elf: Health = {CurrentHealth}; Agility = {Agility};");
        }
    }
}