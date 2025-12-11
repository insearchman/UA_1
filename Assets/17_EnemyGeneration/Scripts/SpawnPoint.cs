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

        private void Start()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            Enemy enemy = Instantiate(_enemyPrefab, transform.position + _enemyPrefab.transform.localPosition, Quaternion.identity);
            enemy.SetBehaviours(_idleBehaviour, _activeBehaviour);
        }
    }
}