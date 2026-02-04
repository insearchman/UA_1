using System;
using UnityEngine;

namespace Modul_28_3
{
    public enum KeyboardComands
    {
        SpawnUnit1,
        SpawnUnit2,
        SpawnUnit3
    }

    public class KeyboardInput : MonoBehaviour
    {
        public event Action<KeyboardComands> ButtonPressed;

        [SerializeField] private KeyCode _spawnUnit1Key = KeyCode.Alpha1;
        [SerializeField] private KeyCode _spawnUnit2Key = KeyCode.Alpha2;
        [SerializeField] private KeyCode _spawnUnit3Key = KeyCode.Alpha3;

        private void Update()
        {
            InputHandler();
        }

        private void InputHandler()
        {
            if (Input.GetKeyDown(_spawnUnit1Key))
                ButtonPressed?.Invoke(KeyboardComands.SpawnUnit1);
            if (Input.GetKeyDown(_spawnUnit2Key))
                ButtonPressed?.Invoke(KeyboardComands.SpawnUnit2);
            if (Input.GetKeyDown(_spawnUnit3Key))
                ButtonPressed?.Invoke(KeyboardComands.SpawnUnit3);
        }
    }
}