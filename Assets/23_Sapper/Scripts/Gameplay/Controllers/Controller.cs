namespace Modul_23.Gameplay
{
    public abstract class Controller
    {
        private bool _isEnabled;

        public bool IsActive { get; protected set; }

        public void Update(float deltaTime)
        {
            if (_isEnabled == false)
                return;

            UpdateLogic(deltaTime);
        }

        public virtual void Enable() => _isEnabled = true;
        public virtual void Disable() => _isEnabled = false;
        public virtual void SetActive(bool isActive) => IsActive = isActive;

        protected abstract void UpdateLogic(float deltaTime);
    }
}