using UnityEngine;

namespace Modul_17
{
    public enum IdleBehaviourTypes
    {
        Stay,
        Patrol,
        Roam
    }

    public enum ActiveBehaviourTypes
    {
        RunAway,
        Pursue,
        Die
    }

    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private PatrolArea _patrolArea;

        [SerializeField] private IdleBehaviourTypes _idleBehaviour;
        [SerializeField] private ActiveBehaviourTypes _activeBehaviour;

        private Enemy _enemy;

        private void Start()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            _enemy = Instantiate(_enemyPrefab, transform.position + _enemyPrefab.transform.localPosition, Quaternion.identity);

            IBehaviour idle = GetIdleBehaviour(_idleBehaviour);
            IBehaviour active = GetActiveBehaviour(_activeBehaviour);

            _enemy.SetBehaviours(idle, active);
        }

        public IBehaviour GetIdleBehaviour(IdleBehaviourTypes idleBehaviour)
        {
            IBehaviour behaviour = null;

            switch (idleBehaviour)
            {
                case IdleBehaviourTypes.Stay:
                    behaviour = new Stay();
                    break;
                case IdleBehaviourTypes.Patrol:
                    behaviour = new Patrol(_enemy, _patrolArea);
                    break;
                case IdleBehaviourTypes.Roam:
                    behaviour = new Roam(_enemy);
                    break;
                default:
                    Debug.Log("SetBehaviours: Not valid IdleBehaviour");
                    break;
            }

            return behaviour;
        }

        public IBehaviour GetActiveBehaviour(ActiveBehaviourTypes activeBehaviour)
        {
            IBehaviour behaviour = null;

            switch (activeBehaviour)
            {
                case ActiveBehaviourTypes.RunAway:
                    behaviour = new RunAway(_enemy);
                    break;
                case ActiveBehaviourTypes.Pursue:
                    behaviour = new Pursue(_enemy);
                    break;
                case ActiveBehaviourTypes.Die:
                    behaviour = new Suicide(_enemy);
                    break;
                default:
                    Debug.Log("SetBehaviours: Not valid ActionBehaviour");
                    break;
            }

            return behaviour;
        }
    }
}