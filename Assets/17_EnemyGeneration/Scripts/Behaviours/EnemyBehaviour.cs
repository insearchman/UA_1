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

        private Mover _mover;
        private Transform _self;
        private PatrolArea _patrolArea;
        private List<Transform> _patrolPoints;

        private int _currentPatrolPointIndex;
        private float _walkingSpeed;
        private float _rotationSpeed;

        public Patrol(Mover mover, PatrolArea patrolArea)
        {
            _mover = mover;
            _self = _mover.transform;
            _walkingSpeed = _self.transform.GetComponent<Enemy>().WalkingSpeed;
            _rotationSpeed = _self.transform.transform.GetComponent<Enemy>().RotationSpeed;

            _patrolArea = patrolArea;
            _patrolPoints = _patrolArea.Points;

            _currentPatrolPointIndex = GetRandomPatrolPoint();
        }

        public void Update()
        {
            Vector3 currentDirectionToMove = GetDirectionToMove();
            _mover.MoveCharacter(currentDirectionToMove, _walkingSpeed, _rotationSpeed);
        }

        public Vector3 GetDirectionToMove()
        {
            Vector3 vectorToTarget = _patrolPoints[_currentPatrolPointIndex].transform.position - _self.transform.position;
            vectorToTarget.y = 0;

            float distance = Mathf.Abs(vectorToTarget.magnitude);

            if (distance <= MinDistance)
                _currentPatrolPointIndex = GetRandomPatrolPoint();

            Vector3 direction = (_patrolPoints[_currentPatrolPointIndex].transform.position - _self.transform.position).normalized;
            direction.y = 0;

            return direction.normalized;
        }

        private int GetRandomPatrolPoint()
        {
            int newPoint;

            do
                newPoint = Random.Range(0, _patrolPoints.Count);
            while (_currentPatrolPointIndex == newPoint);

            return newPoint;
        }
    }

    public class Roam : IBehaviour
    {
        private const float ChangeDirectionTime = 1f;

        private float _time;

        private Vector3 _currentMoveDirection;

        private Mover _mover;

        private float _walkingSpeed;
        private float _rotationSpeed;

        public Roam(Mover mover)
        {
            _mover = mover;
            _currentMoveDirection = CreateRandomMoveDirection();

            _walkingSpeed = _mover.transform.GetComponent<Enemy>().WalkingSpeed;
            _rotationSpeed = _mover.transform.transform.GetComponent<Enemy>().RotationSpeed;
        }

        public void Update()
        {
            Vector3 currentDirectionToMove = GetDirectionToMove();
            _mover.MoveCharacter(currentDirectionToMove, _walkingSpeed, _rotationSpeed);
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
        private Mover _mover;
        private Transform _self;
        private Player _target;
        private Enemy _enemy;

        private float _walkingSpeed;
        private float _rotationSpeed;

        public RunAway(Mover mover)
        {
            _mover = mover;
            _self = _mover.transform;

            _enemy = _self.transform.GetComponent<Enemy>();
            _walkingSpeed = _self.transform.GetComponent<Enemy>().WalkingSpeed;
            _rotationSpeed = _self.transform.transform.GetComponent<Enemy>().RotationSpeed;
        }

        public void Update()
        {
            _target = _enemy.Player;

            if (_target == null)
                return;

            Vector3 currentDirectionToMove = GetDirectionToMove(_target.gameObject);
            _mover.MoveCharacter(currentDirectionToMove, _walkingSpeed, _rotationSpeed);
        }

        private Vector3 _targetDirection;

        public Vector3 GetDirectionToMove(GameObject player)
        {
            _targetDirection = _self.transform.position - player.transform.position;
            _targetDirection.y = 0;

            return _targetDirection.normalized;
        }
    }

    public class Pursue : IBehaviour
    {
        private Mover _mover;
        private Transform _self;
        private Player _target;
        private Enemy _enemy;

        private float _walkingSpeed;
        private float _rotationSpeed;

        public Pursue(Mover mover)
        {
            _mover = mover;
            _self = _mover.transform;

            _enemy = _self.transform.GetComponent<Enemy>();
            _walkingSpeed = _self.transform.GetComponent<Enemy>().WalkingSpeed;
            _rotationSpeed = _self.transform.transform.GetComponent<Enemy>().RotationSpeed;
        }

        public void Update()
        {
            _target = _enemy.Player;

            if (_target == null)
                return;

            Vector3 currentDirectionToMove = GetDirectionToMove(_target.gameObject);
            _mover.MoveCharacter(currentDirectionToMove, _walkingSpeed, _rotationSpeed);
        }

        private Vector3 _targetDirection;

        public Vector3 GetDirectionToMove(GameObject player)
        {
            _targetDirection = player.transform.position - _self.transform.position;
            _targetDirection.y = 0;

            return _targetDirection.normalized;
        }
    }

    public class Die : IBehaviour
    {
        private Mover _mover;
        private Enemy _self;

        public Die(Mover mover)
        {
            _mover = mover;
            _self = _mover.GetComponent<Enemy>();
        }

        public void Update()
        {
            _self.Die();
        }
    }
}