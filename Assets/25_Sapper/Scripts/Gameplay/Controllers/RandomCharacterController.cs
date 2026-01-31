using Modul_25.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Modul_25.Gameplay
{
    public class RandomCharacterController : Controller
    {
        private const int StartCornerIndex = 0;
        private const int TargetCornerIndex = 1;
        private const int MinCornersCountInPathToMove = 2;
        private const float MinDistanceToMove = 0.05f;
        private const float DistanceToWalk = 4f;

        private const float TimeIdle = 4f;

        private float _time;

        private Character _character;
        private NavMeshPath _path;
        private Vector3 _movePoint;
        private NavMeshQueryFilter _filter;

        public RandomCharacterController(Character character, NavMeshQueryFilter filter)
        {
            _path = new NavMeshPath();

            _character = character;
            _filter = filter;

            _time = TimeIdle;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (IsActive)
            {
                if (_character is AgentCharacter agent)
                {
                    if (Jump(agent))
                        return;

                    if (TryGetMovePoint(agent))
                    {
                        Vector3 vectorToNextCorner = _path.corners[TargetCornerIndex] - _path.corners[StartCornerIndex];
                        agent.SetMovePoint(_movePoint);
                        agent.SetMoveDirection(vectorToNextCorner);
                        agent.ResumeMove();
                    }
                    else
                    {
                        agent.SetMoveDirection(Vector3.zero);
                        agent.StopMove();
                    }
                }
                else
                {
                    Vector3 moveDirection = GetVectorToNextCorner();
                    _character.SetMoveDirection(moveDirection);
                }

                _time += deltaTime;

                if (_time > TimeIdle)
                {
                    _time = 0;

                    _movePoint = GetRandomMovePoint();
                }
            }
            else
            {
                _time = TimeIdle;
            }
        }

        public override void SetActive(bool isActive)
        {
            if (IsActive == false & isActive)
                _movePoint = GetRandomMovePoint();

            base.SetActive(isActive);
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

        private bool TryGetMovePoint(AgentCharacter agent)
        {
            if (agent.TryGetPath(_movePoint, _path))
            {
                float distanceToTarget = NavMeshUtils.GetPathLength(_path);

                if (IsDistanceEnough(distanceToTarget))
                {
                    return true;
                }
            }

            return false;
        }

        private Vector3 GetRandomMovePoint()
        {
            Vector3 movePoint;

            do
            {
                movePoint = _character.transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * DistanceToWalk;
            }
            while (NavMeshUtils.TryGetPath(_character.transform.position, movePoint, _filter, _path) == false);
            
            return movePoint;
        }

        private Vector3 GetVectorToNextCorner()
        {
            if (NavMeshUtils.TryGetPath(_character.transform.position, _movePoint, _filter, _path))
            {
                float distanceToTarget = NavMeshUtils.GetPathLength(_path);

                if (IsDistanceEnough(distanceToTarget) && EnoughCornerInPath(_path))
                {
                    return _path.corners[TargetCornerIndex] - _path.corners[StartCornerIndex];
                }
            }

            return Vector3.zero;
        }

        private bool IsDistanceEnough(float distance) => distance > MinDistanceToMove;
        private bool EnoughCornerInPath(NavMeshPath path) => path.corners.Length >= MinCornersCountInPathToMove;
    }
}