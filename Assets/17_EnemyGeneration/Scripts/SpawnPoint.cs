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
        [SerializeField] private BehaviourController _enemyPrefab;

        [SerializeField] private IdleBehaviourTypes _idleBehaviour;
        [SerializeField] private ActiveBehaviourTypes _activeBehaviour;

        private void Awake()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            BehaviourController enemy = Instantiate(_enemyPrefab, transform.position + _enemyPrefab.transform.localPosition, Quaternion.identity);
            enemy.SetBehaviours(_idleBehaviour, _activeBehaviour);
        }
    }
}