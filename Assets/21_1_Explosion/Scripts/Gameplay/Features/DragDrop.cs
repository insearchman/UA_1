using UnityEngine;

namespace Modul_21_1.Gameplay
{
    public class DragDrop : IHoldButtonHandler
    {
        private readonly LayerMask InteractionMask;
        private readonly LayerMask FloorMask;

        private readonly Vector3 DragHeightOffset = new Vector3(0, 1.5f, 0);

        private IDragable _target;

        public DragDrop(LayerMask interactionMask, LayerMask floorMask)
        {
            InteractionMask = interactionMask;
            FloorMask = floorMask;
        }

        public void OnButtonDown()
        {
            Ray rayFromCamera = GetMousePositionRay();

            if (Physics.Raycast(rayFromCamera, out RaycastHit targetHit, Mathf.Infinity, InteractionMask))
            {
                if (targetHit.transform.TryGetComponent(out IDragable dragable))
                {
                    _target = dragable;
                    _target.OnGrab();
                }
            }
        }

        public void OnButtonHold()
        {
            if (_target == null)
                return;

            Ray rayFromCamera = GetMousePositionRay();

            if (TryGetPointOnFloor(rayFromCamera, out Vector3 positionOnFloor))
            {
                _target.OnDrag(positionOnFloor + DragHeightOffset);
            }
        }

        public void OnButtonRelease()
        {
            if (_target == null)
                return;

            _target.OnRelease();
            _target = null;
        }

        private Ray GetMousePositionRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private bool TryGetPointOnFloor(Ray mousePosition, out Vector3 dragPosition)
        {
            dragPosition = Vector3.zero;

            if (Physics.Raycast(mousePosition, out RaycastHit hit, Mathf.Infinity, FloorMask))
            {
                dragPosition = hit.point;
                return true;
            }

            return false;
        }
    }
}