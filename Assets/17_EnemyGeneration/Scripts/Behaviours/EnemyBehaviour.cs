using System.Collections.Generic;
using UnityEngine;

namespace Modul_17
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        protected const float MinDistance = 0.5f;
    }

    public class Stay : EnemyBehaviour
    {

    }

    public class Patrol : EnemyBehaviour, ISimpleMovable
    {
        private PatrolArea _patrolArea;
        private List<Transform> _patrolPoints;

        private int _currentPatrolPointIndex;

        private void Start()
        {
            _patrolArea = FindFirstObjectByType<PatrolArea>();
            _patrolPoints = _patrolArea.Points;

            _currentPatrolPointIndex = GetRandomPatrolPoint();
        }

        public Vector3 GetDirectionToMove()
        {
            Vector3 vectorToTarget = _patrolPoints[_currentPatrolPointIndex].transform.position - transform.position;
            vectorToTarget.y = 0;

            float distance = Mathf.Abs(vectorToTarget.magnitude);

            if (distance <= MinDistance)
                _currentPatrolPointIndex = GetRandomPatrolPoint();

            Vector3 direction = (_patrolPoints[_currentPatrolPointIndex].transform.position - transform.position).normalized;
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

    public class Roam : EnemyBehaviour, ISimpleMovable
    {
        private const float ChangeDirectionTime = 1f;

        private float _time;

        private Vector3 _currentMoveDirection;

        private void Awake()
        {
            _currentMoveDirection = CreateRandomMoveDirection();
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

    public class RunAway : EnemyBehaviour, ITargetMovable
    {
        private Vector3 _targetDirection;

        public Vector3 GetDirectionToMove(GameObject player)
        {
            _targetDirection = transform.position - player.transform.position;
            _targetDirection.y = 0;

            return _targetDirection.normalized;
        }
    }

    public class Pursue : EnemyBehaviour, ITargetMovable
    {
        private Vector3 _targetDirection;

        public Vector3 GetDirectionToMove(GameObject player)
        {
            _targetDirection = player.transform.position - transform.position;
            _targetDirection.y = 0;

            return _targetDirection.normalized;
        }
    }

    public class Die : EnemyBehaviour, IEffectAction
    {
        public void Action(ParticleSystem particle)
        {
            if (particle != null)
            {
                ParticleSystem effect = Instantiate(particle, transform.position, particle.transform.rotation);
                effect.transform.SetParent(null);
                effect.Play();

                Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
            }

            Destroy(gameObject);
        }
    }
}