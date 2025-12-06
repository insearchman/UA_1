using UnityEngine;

namespace Modul_17
{
    public class BehaviourController : MonoBehaviour
    {
        public EnemyBehaviour IdleBehaviour { get; private set; }
        public EnemyBehaviour ActiveBehaviour { get; private set; }

        public void SetBehaviours(IdleBehaviourTypes idleBehaviour, ActiveBehaviourTypes activeBehaviour)
        {
            switch (idleBehaviour)
            {
                case IdleBehaviourTypes.Stay:
                    IdleBehaviour = gameObject.AddComponent<Stay>();
                    break;
                case IdleBehaviourTypes.Patrol:
                    IdleBehaviour = gameObject.AddComponent<Patrol>();
                    break;
                case IdleBehaviourTypes.Roam:
                    IdleBehaviour = gameObject.AddComponent<Roam>();
                    break;
                default:
                    Debug.Log("SetBehaviours: Not valid IdleBehaviour");
                    break;
            }

            switch (activeBehaviour)
            {
                case ActiveBehaviourTypes.RunAway:
                    ActiveBehaviour = gameObject.AddComponent<RunAway>();
                    break;
                case ActiveBehaviourTypes.Pursue:
                    ActiveBehaviour = gameObject.AddComponent<Pursue>();
                    break;
                case ActiveBehaviourTypes.Die:
                    ActiveBehaviour = gameObject.AddComponent<Die>();
                    break;
                default:
                    Debug.Log("SetBehaviours: Not valid ActionBehaviour");
                    break;
            }
        }
    }
}