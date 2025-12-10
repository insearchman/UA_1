using UnityEngine;

namespace Modul_21_1
{
    public class DragDrop : MonoBehaviour
    {
        [SerializeField] private LayerMask _interactionMask;
        [SerializeField] private LayerMask _floorMask;

        private readonly Vector3 DragHeightOffset = new Vector3(0, 1.5f, 0);

        private GameObject _target;

        private bool _isMissClickByTarget;

        public void DragTarget(bool isDragDropPressed)
        {
            if (isDragDropPressed)
            {
                if (_isMissClickByTarget)
                    return;

                Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (_target == null)
                {
                    DetectTarget(rayFromCamera);
                }
                else
                {
                    SetTargetDragPosition(rayFromCamera);
                }
            }
            else
            {
                if (_target == null)
                {
                    _isMissClickByTarget = false;
                    return;
                }

                DropTarget();
            }
        }

        private void DetectTarget(Ray rayToCheck)
        {
            if (Physics.Raycast(rayToCheck, out RaycastHit targetHit, Mathf.Infinity, _interactionMask))
            {
                _target = targetHit.transform.gameObject;
                _target.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                _isMissClickByTarget = true;
            }
        }

        private void SetTargetDragPosition(Ray rayToCheck)
        {
            if (TryGetPointOnFloor(rayToCheck, out Vector3 positionOnFloor))
            {
                _target.transform.position = positionOnFloor + DragHeightOffset;
            }
        }

        private void DropTarget()
        {
            _target.GetComponent<Rigidbody>().isKinematic = false;
            _target = null;
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