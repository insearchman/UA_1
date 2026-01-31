using Modul_25.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Modul_25.Gameplay
{
    public class AgentCharacter : Character
    {
        private NavMeshAgent _agent;
        private AgentMover _agentMover;
        private AgentJumper _jumper;

        private bool _isAgentMoverActive;
        private bool _isAgentActive;

        [SerializeField] private AnimationCurve _jumpCurve;

        public bool InJumpProcess => _jumper.InProcess;
        public float JumpDuration {  get; private set; }

        protected override void Awake()
        {
            base.Awake();

            if (TryGetComponent(out NavMeshAgent agent))
            {
                _agent = agent;
                _agentMover = new AgentMover(_agent, transform, MoveSpeed, RotateSpeed);
                _mover = _agentMover;

                _jumper = new AgentJumper(JumpSpeed, _agent, this, _jumpCurve);
            }
            else
            {
                Debug.Log("No move component");
            }
        }

        protected override void UpdateMover()
        {
            if(_isAgentActive)
                base.UpdateMover();
            if (_isAgentMoverActive)
                _agentMover.Update(Time.deltaTime);
        }

        private void SetAgentMoverActive(bool isActive)
        {
            _isAgentMoverActive = isActive;
            _isAgentActive = !isActive;
        }

        public void SetMovePoint(Vector3 movePoint)
        {
            SetAgentMoverActive(true);
            _agentMover.SetMovePoint(movePoint);
        }

        public override void SetMoveDirection(Vector3 moveDirection)
        {
            SetAgentMoverActive(false);
            base.SetMoveDirection(moveDirection);
        }

        public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) => 
            NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

        public bool IsOnNavMeshLink (out OffMeshLinkData offMeshLinkData)
        {
            if (_agent.isOnOffMeshLink)
            {
                offMeshLinkData = _agent.currentOffMeshLinkData;
                return true;
            }

            offMeshLinkData = default;
            return false;
        }

        public void Jump(OffMeshLinkData offMeshLinkData)
        {
            _jumper.Jump(offMeshLinkData);
            JumpDuration = _jumper.JumpDuration;
        }

        public void StopMove() => _agentMover.Stop();
        public void ResumeMove() => _agentMover.Resume();

    }
}