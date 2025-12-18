using System.Collections.Generic;
using UnityEngine;

namespace Modul_17
{
    public interface IBehaviour
    {
        public void Update();
    }

    public class Stay : IBehaviour
    {
        public void Update() { }
    }

    public class Patrol : IBehaviour
    {
        private const float MinDistance = 0.5f;

        private readonly float WalkingSpeed;
        private readonly float RotationSpeed;
        private readonly Mover MovableMover;
        private readonly Transform MovableTransform;
        private readonly List<Transform> PatrolPoints;

        private int _currentPatrolPointIndex;

        public Patrol(IMovable movable, PatrolArea patrolArea)
        {
            WalkingSpeed = movable.WalkingSpeed;
            RotationSpeed = movable.RotationSpeed;
            MovableMover = movable.Mover;
            MovableTransform = movable.Transform;

            PatrolPoints = patrolArea.Points;
            _currentPatrolPointIndex = GetRandomPatrolPoint();
        }

        public void Update()
        {
            Vector3 currentDirectionToMove = GetDirectionToMove();
            MovableMover.MoveCharacter(currentDirectionToMove, WalkingSpeed, RotationSpeed);
        }

        public Vector3 GetDirectionToMove()
        {
            Vector3 vectorToTarget = PatrolPoints[_currentPatrolPointIndex].position - MovableTransform.position;
            vectorToTarget.y = 0;

            float distance = Mathf.Abs(vectorToTarget.magnitude);

            if (distance <= MinDistance)
                _currentPatrolPointIndex = GetRandomPatrolPoint();

            Vector3 direction = (PatrolPoints[_currentPatrolPointIndex].position - MovableTransform.position).normalized;
            direction.y = 0;

            return direction.normalized;
        }
        
        private int GetRandomPatrolPoint()
        {
            int newPoint;

            do
                newPoint = Random.Range(0, PatrolPoints.Count);
            while (_currentPatrolPointIndex == newPoint);

            return newPoint;
        }
    }

    public class Roam : IBehaviour
    {
        private const float ChangeDirectionTime = 1f;

        private readonly float WalkingSpeed;
        private readonly float RotationSpeed;
        private readonly Mover MovableMover;

        private float _time;
        private Vector3 _currentMoveDirection;

        public Roam(IMovable movable)
        {
            WalkingSpeed = movable.WalkingSpeed;
            RotationSpeed = movable.RotationSpeed;
            MovableMover = movable.Mover;

            _currentMoveDirection = CreateRandomMoveDirection();
        }

        public void Update()
        {
            Vector3 currentDirectionToMove = GetDirectionToMove();
            MovableMover.MoveCharacter(currentDirectionToMove, WalkingSpeed, RotationSpeed);
        }

        public Vector3 GetDirectionToMove()
        {
            _time += Time.deltaTime;

            if (_time > ChangeDirectionTime)
            {
                _time = 0;
                _currentMoveDirection = CreateRandomMoveDirection();
            }

            return _currentMoveDirection;
        }

        private Vector3 CreateRandomMoveDirection()
        {
            Vector3 direction;

            do
                direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            while (direction == Vector3.zero);

            return direction;
        }
    }

    public class RunAway : IBehaviour
    {
        private readonly IMovable Movable;
        private readonly float WalkingSpeed;
        private readonly float RotationSpeed;
        private readonly Mover MovableMover;
        private readonly Transform MovableTransform;

        private Transform _currentTarget;

        public RunAway(IMovable movable)
        {
            Movable = movable;

            WalkingSpeed = Movable.WalkingSpeed;
            RotationSpeed = Movable.RotationSpeed;
            MovableMover = Movable.Mover;
            MovableTransform = Movable.Transform;
        }

        public void Update()
        {
            _currentTarget = Movable.Target.transform;

            if (_currentTarget == null)
                return;

            Vector3 currentDirectionToMove = GetDirectionToMove(_currentTarget);
            MovableMover.MoveCharacter(currentDirectionToMove, WalkingSpeed, RotationSpeed);
        }

        public Vector3 GetDirectionToMove(Transform player)
        {
            Vector3 _targetDirection;

            _targetDirection = MovableTransform.position - player.position;
            _targetDirection.y = 0;

            return _targetDirection.normalized;
        }
    }

    public class Pursue : IBehaviour
    {
        private readonly IMovable Movable;
        private readonly float WalkingSpeed;
        private readonly float RotationSpeed;
        private readonly Mover MovableMover;
        private readonly Transform MovableTransform;

        private Transform _currentTarget;

        public Pursue(IMovable movable)
        {
            Movable = movable;
            MovableMover = Movable.Mover;
            MovableTransform = Movable.Transform;
            WalkingSpeed = Movable.WalkingSpeed;
            RotationSpeed = Movable.RotationSpeed;
        }

        public void Update()
        {
            _currentTarget = Movable.Target.transform;

            if (_currentTarget == null)
                return;

            Vector3 currentDirectionToMove = GetDirectionToMove(_currentTarget);
            MovableMover.MoveCharacter(currentDirectionToMove, WalkingSpeed, RotationSpeed);
        }

        public Vector3 GetDirectionToMove(Transform player)
        {
            Vector3 _targetDirection;

            _targetDirection = player.position - MovableTransform.position;
            _targetDirection.y = 0;

            return _targetDirection.normalized;
        }
    }

    public class Suicide : IBehaviour
    {
        private readonly IMovable _mover;

        public Suicide(IMovable movable)
        {
            _mover = movable;
        }

        public void Update()
        {
            _mover.Die();
        }
    }
}