using UnityEngine;
using UnityEngine.AI;

using Modul_25.Utils;

namespace Modul_25.Gameplay
{
    public class PointCharacterController : Controller
    {
        private const KeyCode PointKey = KeyCode.Mouse0;

        private const int StartCornerIndex = 0;
        private const int TargetCornerIndex = 1;
        private const int MinCornersCountInPathToMove = 2;
        private const float MinDistanceToMove = 0.05f;

        private Character _character;
        private NavMeshPath _path;
        private Transform _pointFlagPrefab;
        private NavMeshQueryFilter _filter;
        private LayerMask _floorMask;
        private float _timeToBored;

        private Vector3 _floorPoint;
        private float _inactiveTime;

        public PointCharacterController(Character character, LayerMask floorMask, NavMeshQueryFilter filter, Transform pointFlagPrefab, float timeToBored)
        {
            _path = new NavMeshPath();

            _character = character;
            _floorMask = floorMask;
            _filter = filter;
            _pointFlagPrefab = pointFlagPrefab;
            _timeToBored = timeToBored;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            _floorPoint = UpdateFloorPoint();

            if (!IsActive)
            {
                _pointFlagPrefab.gameObject.SetActive(false);
                return;
            }

            SleepBehaviour(deltaTime);

            if (_character is AgentCharacter agent)
            {
                if (Jump(agent))
                    return;

                if (TryGetMovePoint(agent, out Vector3 movePoint))
                {
                    _pointFlagPrefab.position = _floorPoint;
                    _pointFlagPrefab.gameObject.SetActive(true);

                    Vector3 vectorToNextCorner = _path.corners[TargetCornerIndex] - _path.corners[StartCornerIndex];
                    agent.SetMovePoint(movePoint);
                    agent.SetMoveDirection(vectorToNextCorner);

                    agent.ResumeMove();
                }
                else
                {
                    _pointFlagPrefab.gameObject.SetActive(false);

                    agent.SetMoveDirection(Vector3.zero);
                    agent.StopMove();
                }
            }
            else
            {
                Vector3 moveVector = GetVectorToNextCorner();
                _character.SetMoveDirection(moveVector);
            }
        }

        private bool Jump(AgentCharacter agent)
        {
            if (agent.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
            {
                if (agent.InJumpProcess == false)
                {
                    agent.Jump(offMeshLinkData);
                }

                return true;
            }

            return false;
        }

        private void SleepBehaviour(float deltaTime)
        {
            if (_character.IsMoved)
            {
                _inactiveTime = 0;
            }
            else
            {
                _inactiveTime += deltaTime;
            }

            if (_inactiveTime > _timeToBored)
            {
                IsActive = false;
            }
        }

        private Vector3 UpdateFloorPoint()
        {
            if (Input.GetKeyDown(PointKey))
            {
                _inactiveTime = 0;
                IsActive = true;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _floorMask))
                {
                    return hit.point;
                }
            }

            return _floorPoint;
        }

        private Vector3 GetVectorToNextCorner()
        {
            if (NavMeshUtils.TryGetPath(_character.transform.position, _floorPoint, _filter, _path))
            {
                float distanceToTarget = NavMeshUtils.GetPathLength(_path);

                if (IsDistanceEnough(distanceToTarget) && EnoughCornerInPath(_path))
                {
                    return _path.corners[TargetCornerIndex] - _path.corners[StartCornerIndex];
                }
            }

            return Vector3.zero;
        }

        private bool TryGetMovePoint(AgentCharacter agent, out Vector3 point)
        {
            point = Vector3.zero;

            if (agent.TryGetPath(_floorPoint, _path))
            {
                float distanceToTarget = NavMeshUtils.GetPathLength(_path);

                if (IsDistanceEnough(distanceToTarget))
                {
                    point = _floorPoint;
                    return true;
                }
            }

            return false;
        }

        private bool IsDistanceEnough(float distance) => distance > MinDistanceToMove;
        private bool EnoughCornerInPath(NavMeshPath path) => path.corners.Length >= MinCornersCountInPathToMove;
    }
}