using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper
{
    private float _speed;
    private NavMeshAgent _agent;
    private AnimationCurve _yOffsetCurve;

    private MonoBehaviour _coroutineRunner;
    private Coroutine _jumpProcess;

    public AgentJumper(float speed, NavMeshAgent agent, MonoBehaviour coroutineRunner, AnimationCurve yOffsetCurve)
    {
        _speed = speed;
        _agent = agent;
        _coroutineRunner = coroutineRunner;
        _yOffsetCurve = yOffsetCurve;
    }

    public bool InProcess => _jumpProcess != null;
    public float JumpDuration {  get; private set; }

    public void Jump(OffMeshLinkData offMeshLinkData)
    {
        if (InProcess)
            return;

        _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcess(offMeshLinkData));
    }

    private IEnumerator JumpProcess(OffMeshLinkData offMeshLinkData)
    {
        Vector3 startPosition = offMeshLinkData.startPos;
        Vector3 endPosition = offMeshLinkData.endPos;

        JumpDuration = Vector3.Distance(startPosition, endPosition) / _speed;

        float progress = 0;

        while (progress < JumpDuration)
        {
            float yOffset = _yOffsetCurve.Evaluate(progress / JumpDuration);

            _agent.transform.position = 
                Vector3.Lerp(startPosition, endPosition, progress / JumpDuration) + Vector3.up * yOffset;
            progress += Time.deltaTime;
            yield return null;
        }

        _agent.CompleteOffMeshLink();
        _jumpProcess = null;
    }
}