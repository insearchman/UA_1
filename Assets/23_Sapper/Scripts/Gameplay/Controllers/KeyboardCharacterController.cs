using UnityEngine;

namespace Modul_23.Gameplay
{
    public class KeyboardCharacterController : Controller
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        private Character _character;

        public KeyboardCharacterController(Character character)
        {
            _character = character;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            Vector3 direction = new Vector3(Input.GetAxisRaw(HorizontalAxis), 0, Input.GetAxisRaw(VerticalAxis));

            _character.SetMoveDirection(direction);
        }
    }
}