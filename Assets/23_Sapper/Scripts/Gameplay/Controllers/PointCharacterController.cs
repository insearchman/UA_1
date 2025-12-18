using UnityEngine;
using UnityEngine.AI;

using Modul_23.Utils;

namespace Modul_23.Gameplay
{
    public class PointCharacterController : Controller
    {
        private const KeyCode PointKey = KeyCode.Mouse0;

        private const int StartCornerIndex = 0;
        private const int TargetCornerIndex = 1;
        private const int MinCornersCountInPathToMove = 2;
        private const float MinDistanceToMove = 0.05f;

        private readonly Character Target;
        private readonly NavMeshPath Path;
        private readonly Transform PointFlagPrefab;
        private readonly NavMeshQueryFilter Filter;
        private readonly LayerMask FloorMask;
        private readonly float TimeToBored;

        private Vector3 _movePoint;
        private float _inactiveTime;

        public PointCharacterController(Character target, LayerMask floorMask, NavMeshQueryFilter filter, Transform pointFlagPrefab, float timeToBored)
        {
            Path = new NavMeshPath();

            Target = target;
            FloorMask = floorMask;
            Filter = filter;
            PointFlagPrefab = pointFlagPrefab;
            TimeToBored = timeToBored;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            SetMovePoint();

            if (IsActive)
            {
                SleepBehaviour(deltaTime);

                Vector3 movePoint = GetMovePoint();
                Target.SetMoveDirection(movePoint);
            }
        }

        private void SleepBehaviour(float deltaTime)
        {
            if (Target.IsMoved)
            {
                _inactiveTime = 0;
            }
            else
            {
                _inactiveTime += deltaTime;
            }

            if (_inactiveTime > TimeToBored)
            {
                IsActive = false;
            }
        }

        private void SetMovePoint()
        {
            if (Input.GetKeyDown(PointKey))
            {
                _inactiveTime = 0;
                IsActive = true;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, FloorMask))
                {
                    _movePoint = hit.point;
                }
            }
        }

        private Vector3 GetMovePoint()
        {
            if (NavMeshUtils.TryGetPath(Target.transform.position, _movePoint, Filter, Path))
            {
                float distanceToTarget = NavMeshUtils.GetPathLength(Path);

                if (IsDistanceEnough(distanceToTarget) && EnoughCornerInPath(Path))
                {
                    PointFlagPrefab.position = _movePoint;
                    PointFlagPrefab.gameObject.SetActive(true);
                    
                    return Path.corners[TargetCornerIndex] - Path.corners[StartCornerIndex];
                }
            }

            PointFlagPrefab.gameObject.SetActive(false);

            return Vector3.zero;
        }

        private bool IsDistanceEnough(float distance) => distance > MinDistanceToMove;
        private bool EnoughCornerInPath(NavMeshPath path) => path.corners.Length >= MinCornersCountInPathToMove;
    }
}