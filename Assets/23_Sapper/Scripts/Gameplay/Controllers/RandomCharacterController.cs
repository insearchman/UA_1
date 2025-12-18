using UnityEngine;
using UnityEngine.AI;

using Modul_23.Utils;

namespace Modul_23.Gameplay
{
    public class RandomCharacterController : Controller
    {
        private const int StartCornerIndex = 0;
        private const int TargetCornerIndex = 1;
        private const int MinCornersCountInPathToMove = 2;
        private const float MinDistanceToMove = 0.05f;
        private const float DistanceToWalk = 4f;

        private const float TimeIdle = 4f;

        private readonly Character Target;
        private readonly NavMeshPath Path;
        private readonly NavMeshQueryFilter Filter;

        private float _time;

        private Vector3 _movePoint;

        public RandomCharacterController(Character target, NavMeshQueryFilter filter)
        {
            Path = new NavMeshPath();

            Target = target;
            Filter = filter;

            _time = TimeIdle;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (IsActive)
            {
                _time += deltaTime;

                if (_time > TimeIdle)
                {
                    _time = 0;

                    _movePoint = GetRandomPoint();
                }

                Vector3 moveDirection = GetMoveDirection();
                Target.SetMoveDirection(moveDirection);
            }
            else
            {
                _time = TimeIdle;
            }
        }

        public override void SetActive(bool isActive)
        {
            if (IsActive == false & isActive)
                _movePoint = GetRandomPoint();

            base.SetActive(isActive);
        }

        private Vector3 GetRandomPoint()
        {
            Vector3 newMovePoint;

            do
            {
                Vector3 newMoveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * DistanceToWalk;
                newMovePoint = Target.transform.position + newMoveDirection;
            }
            while (NavMeshUtils.TryGetPath(Target.transform.position, newMovePoint, Filter, Path) == false);
            
            return newMovePoint;
        }

        private Vector3 GetMoveDirection()
        {
            if (NavMeshUtils.TryGetPath(Target.transform.position, _movePoint, Filter, Path))
            {
                float distanceToTarget = NavMeshUtils.GetPathLength(Path);

                if (IsDistanceEnough(distanceToTarget) && EnoughCornerInPath(Path))
                    return Path.corners[TargetCornerIndex] - Path.corners[StartCornerIndex];
            }

            return Vector3.zero;
        }

        private bool IsDistanceEnough(float distance) => distance > MinDistanceToMove;
        private bool EnoughCornerInPath(NavMeshPath path) => path.corners.Length >= MinCornersCountInPathToMove;
    }
}