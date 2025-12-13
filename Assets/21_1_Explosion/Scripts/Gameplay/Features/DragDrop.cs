using UnityEngine;

using Modul_21_1.Gameplay.Interface;

namespace Modul_21_1.Gameplay.Features
{
    public class DragDrop : IHoldButton
    {
        private LayerMask _interactionMask;
        private LayerMask _floorMask;

        private readonly Vector3 DragHeightOffset = new Vector3(0, 1.5f, 0);

        private Transform _target;

        public DragDrop(LayerMask interactionMask, LayerMask floorMask)
        {
            _interactionMask = interactionMask;
            _floorMask = floorMask;
        }

        public void OnButtonDown()
        {
            Ray rayFromCamera = GetMousePositionRay();

            if (Physics.Raycast(rayFromCamera, out RaycastHit targetHit, Mathf.Infinity, _interactionMask))
            {
                _target = targetHit.transform;
                _target.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        public void OnButtonHold()
        {
            if (_target == null)
                return;

            Ray rayFromCamera = GetMousePositionRay();

            if (TryGetPointOnFloor(rayFromCamera, out Vector3 positionOnFloor))
            {
                _target.position = positionOnFloor + DragHeightOffset;
            }
        }

        public void OnButtonRelease()
        {
            if (_target == null)
                return;

            _target.GetComponent<Rigidbody>().isKinematic = false;
            _target = null;
        }

        private Ray GetMousePositionRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private bool TryGetPointOnFloor(Ray mousePosition, out Vector3 dragPosition)
        {
            dragPosition = Vector3.zero;

            if (Physics.Raycast(mousePosition, out RaycastHit hit, Mathf.Infinity, _floorMask))
            {
                dragPosition = hit.point;
                return true;
            }

            return false;
        }
    }
}