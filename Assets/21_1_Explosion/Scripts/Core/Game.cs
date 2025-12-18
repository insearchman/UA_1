using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

using Modul_21_1.Gameplay;

namespace Modul_21_1.Core
{
    public class Game : MonoBehaviour
    {
        private const KeyCode DragDropKey = KeyCode.Mouse0;
        private const KeyCode ExplosionKey = KeyCode.Mouse1;
        private const KeyCode CameraSwitchKey = KeyCode.Space;

        [SerializeField] private LayerMask _interactionMask;
        [SerializeField] private LayerMask _floorMask;

        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private List<CinemachineVirtualCamera> _cameras;

        [SerializeField] private float _explosionRadius = 3f;
        [SerializeField] private float _explosionPowerMultiplier = 300f;

        private IHoldButtonHandler _methodDragDrop;
        private IClickButtonHandler _methodExplosion;
        private IClickButtonHandler _cameraSwitcher;

        private void Start()
        {
            _methodDragDrop = new DragDrop(_interactionMask, _floorMask);
            _methodExplosion = new ExplosionCaster(_interactionMask, _floorMask, _explosionEffect, _explosionRadius, _explosionPowerMultiplier);
            _cameraSwitcher = new CameraSwitcher(_cameras);
        }

        private void Update()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            HoldHandler(DragDropKey, _methodDragDrop);

            ClickHandler(ExplosionKey, _methodExplosion);
            ClickHandler(CameraSwitchKey, _cameraSwitcher);
        }

        private void ClickHandler(KeyCode key, IClickButtonHandler method)
        {
            if (Input.GetKeyDown(key))
                method.OnButtonDown();
        }

        private void HoldHandler(KeyCode key, IHoldButtonHandler method)
        {
            if (Input.GetKeyDown(key))
                method.OnButtonDown();
            else if (Input.GetKey(key))
                method.OnButtonHold();
            else if (Input.GetKeyUp(key))
                method.OnButtonRelease();
        }
    }
}