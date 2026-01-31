using UnityEngine;

namespace Modul_25.Gameplay
{
    public class ActiveIdleControllersManager : Controller
    {
        private Character _target;
        private Controller _activeController;
        private Controller _idleController;

        public ActiveIdleControllersManager(
            Character character, 
            Controller activeController, 
            Controller idleController)
        {
            _target = character;
            _activeController = activeController;
            _idleController = idleController;
        }

        public override void Enable()
        {
            base.Enable();

            _activeController.Enable();
            _idleController.Enable();
        }

        public override void Disable()
        {
            base.Disable();

            _activeController.Disable();
            _idleController.Disable();
        }

        protected override void UpdateLogic(float deltaTime)
        {
            CheckCharacterDeath();

            _activeController.Update(Time.deltaTime);

            if (_activeController.IsActive == false)
                _idleController.SetActive(true);
            else
                _idleController.SetActive(false);

            _idleController.Update(Time.deltaTime);
        }

        private void CheckCharacterDeath()
        {
            if (_target.IsAlive == false)
                Disable();
        }
    }
}