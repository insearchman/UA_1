using UnityEngine;
using UnityEngine.AI;

namespace Modul_25.Gameplay
{
    public class AgentMover : Mover
    {
        private const int AccelerationValue = 999;

        private NavMeshAgent _agent;
        private Transform _transform;

        private Vector3 _movePoint;

        public AgentMover(NavMeshAgent agent, Transform transform, float moveSpeed, float rotateSpeed) : base(moveSpeed, rotateSpeed)
        {
            _agent = agent;
            _transform = transform;

            AgentInit(moveSpeed, rotateSpeed);
        }

        private void AgentInit(float moveSpeed, float rotateSpeed)
        {
            _agent.updateRotation = false;
            _agent.speed = moveSpeed;
            _agent.angularSpeed = rotateSpeed;
            _agent.acceleration = AccelerationValue;
        }

        public override Quaternion CurrentRotation => _transform.rotation;

        public void SetMovePoint(Vector3 movePoint)
        {
            _movePoint = movePoint;
        }

        protected override void ApplyRotation(Quaternion rotation) => _transform.rotation = rotation;
        protected override void Move(float deltaTime) => _agent.SetDestination(_movePoint);

        public void Stop() => _agent.isStopped = true;

        public void Resume() => _agent.isStopped = false;
    }
}