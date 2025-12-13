using Cinemachine;
using System.Collections.Generic;

using Modul_21_1.Gameplay.Interface;

namespace Modul_21_1.Gameplay.Features
{
    public class CameraSwitcher : IClickButton
    {
        private List<CinemachineVirtualCamera> _cameras;

        private Queue<CinemachineVirtualCamera> _cameraQueue;

        public CameraSwitcher(List<CinemachineVirtualCamera> cameras)
        {
            _cameras = cameras;
            _cameraQueue = new Queue<CinemachineVirtualCamera>(_cameras);

            SwitchCamera();
        }

        public void OnButtonDown()
        {
            SwitchCamera();
        }

        public void SwitchCamera()
        {
            CinemachineVirtualCamera nextCamera = _cameraQueue.Dequeue();

            foreach (var camera in _cameras)
            {
                camera.gameObject.SetActive(false);
            }

            nextCamera.gameObject.SetActive(true);

            _cameraQueue.Enqueue(nextCamera);
        }
    }
}