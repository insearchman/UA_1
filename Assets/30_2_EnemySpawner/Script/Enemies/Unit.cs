using UnityEngine;

namespace Modul_30_2
{
    public abstract class Unit : MonoBehaviour
    {

        public int MaxHealth {  get; protected set; }
        public int CurrentHealth { get; protected set; }

        public void Init(UnitProperty property)
        {
            MaxHealth = property.Health;
            CurrentHealth = MaxHealth;

            AdditionalInit(property);
        }

        protected abstract void AdditionalInit(UnitProperty property);
    }
}