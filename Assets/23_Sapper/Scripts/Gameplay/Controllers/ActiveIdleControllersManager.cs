using UnityEngine;

namespace Modul_23.Gameplay
{
    public class ActiveIdleControllersManager : Controller
    {
        private readonly Character Target;
        private readonly Controller ActiveController;
        private readonly Controller IdleController;

        public ActiveIdleControllersManager(Character character, Controller activeController, Controller idleController)
        {
            Target = character;
            ActiveController = activeController;
            IdleController = idleController;
        }

        public override void Enable()
        {
            base.Enable();

            ActiveController.Enable();
            IdleController.Enable();
        }

        public override void Disable()
        {
            base.Disable();

            ActiveController.Disable();
            IdleController.Disable();
        }

        protected override void UpdateLogic(float deltaTime)
        {
            CheckCharacterDeath();

            ActiveController.Update(Time.deltaTime);

            if (ActiveController.IsActive == false)
                IdleController.SetActive(true);
            else
                IdleController.SetActive(false);

            IdleController.Update(Time.deltaTime);
        }

        private void CheckCharacterDeath()
        {
            if (Target.IsAlive == false)
                Disable();
        }
    }
}