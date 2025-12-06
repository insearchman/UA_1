using System;
using UnityEngine;

namespace Modul_17
{
    public class InputKeyboard : InputSystem
    {
        private const KeyCode RunningButton = KeyCode.LeftShift;
        private const KeyCode UseItemButton = KeyCode.F;

        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        private const float DeadZone = 0.1f;

        public override event Action<Vector3> OnMove;
        public override event Action OnStopMove;
        public override event Action OnUseItem;

        private void Update()
        {
            if (TryGetMovementAxis(out Vector3 normalizedinput))
                OnMove?.Invoke(normalizedinput);
            else
                OnStopMove?.Invoke();

            RunningButtonHendler();
            UseItem();
        }

        private bool TryGetMovementAxis(out Vector3 normalizedinput)
        {
            Vector3 input = new Vector3(Input.GetAxisRaw(HorizontalAxis), 0, Input.GetAxisRaw(VerticalAxis));
            normalizedinput = input.normalized;

            if (input.magnitude <= DeadZone)
                return false;

            return true;
        }

        private void RunningButtonHendler()
        {
            IsRunning = Input.GetKey(RunningButton);
        }

        private void UseItem()
        {
            if (Input.GetKeyDown(UseItemButton))
                OnUseItem?.Invoke();
        }
    }
}