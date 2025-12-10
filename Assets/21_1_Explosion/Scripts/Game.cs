using UnityEngine;

namespace Modul_21_1
{
    [RequireComponent(typeof(DragDrop), typeof(ExplosionCaster), typeof(CameraSwitcher))]
    public class Game : MonoBehaviour
    {
        private const KeyCode DragDropKey = KeyCode.Mouse0;
        private const KeyCode ExplosionKey = KeyCode.Mouse1;
        private const KeyCode CameraSwitchKey = KeyCode.Space;

        private DragDrop _draDropMethod;
        private ExplosionCaster _explosion;
        private CameraSwitcher _cameraSwitcher;

        private bool _isDragDropStarted;
        private bool _isExplosion;
        private bool _isCameraSwitch;

        private void Start()
        {
            _draDropMethod = GetComponent<DragDrop>();
            _explosion = GetComponent<ExplosionCaster>();
            _cameraSwitcher = GetComponent<CameraSwitcher>();
        }

        private void Update()
        {
            ReadInput();

            DragDropHandler();
            ExplosionHandler();
            CameraSwitchHandler();
        }

        private void ReadInput()
        {
            if (Input.GetKeyDown(DragDropKey))
                _isDragDropStarted = true;
            else if (Input.GetKeyUp(DragDropKey))
                _isDragDropStarted = false;

            if (Input.GetKeyDown(ExplosionKey))
                _isExplosion = true;

            if (Input.GetKeyDown(CameraSwitchKey))
                _isCameraSwitch = true;
        }

        private void DragDropHandler()
        {
            _draDropMethod.DragTarget(_isDragDropStarted);
        }

        private void ExplosionHandler()
        {
            if (_isExplosion)
            {
                _explosion.Explosion();
                _isExplosion = false;
            }
        }

        private void CameraSwitchHandler()
        {
            if (_isCameraSwitch)
            {
                _cameraSwitcher.SwitchCamera();
                _isCameraSwitch = false;
            }
        }
    }
}