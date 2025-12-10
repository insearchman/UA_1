using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace Modul_21_1
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private List<CinemachineVirtualCamera> _cameras;
        private Queue<CinemachineVirtualCamera> _cameraQueue;

        private void Awake()
        {
            _cameraQueue = new Queue<CinemachineVirtualCamera>(_cameras);

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