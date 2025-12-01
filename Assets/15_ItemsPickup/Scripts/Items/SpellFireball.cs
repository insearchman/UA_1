using UnityEngine;

namespace Modul_15
{
    public class SpellFireball : Item
    {
        private const string DebugMessage = "- Fireball used.";

        private const float ShootDistance = 20f;
        private const float Speed = 20f;

        private Vector3 _startPosition;

        private bool _isActivated = false;

        private void FixedUpdate()
        {
            if (_isActivated)
            {
                if ((transform.position - _startPosition).magnitude < ShootDistance)
                    transform.Translate(transform.forward * Speed * Time.fixedDeltaTime, Space.World);
                else
                    Destroy(gameObject);
            }
        }

        public override void Use(Player player)
        {
            transform.SetParent(null, true);

            _startPosition = transform.position;
            _isActivated = true;

            Debug.Log(DebugMessage);
        }
    }
}