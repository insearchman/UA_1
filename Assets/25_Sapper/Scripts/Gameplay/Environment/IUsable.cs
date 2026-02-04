using UnityEngine;

namespace Modul_25.Gameplay
{
    public interface IUsable
    {
        public void Init(int value);

        public void Use(Character character);
    }
}