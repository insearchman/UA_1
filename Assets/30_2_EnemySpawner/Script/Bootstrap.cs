using UnityEngine;

namespace Modul_30_2
{
    [RequireComponent(typeof(EnemySpawner))]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private int _countOfEnemies = 3;

        [SerializeField] private OrkProperty[] _orkProperties;
        [SerializeField] private ElfProperty[] _elfProperties;
        [SerializeField] private DragonProperty[] _dragonProperties;

        private EnemySpawner _spawner;
        private Vector3 offenStep = new(0,0,1);

        private void Awake()
        {
            _spawner = GetComponent<EnemySpawner>();

            SpawnProcess();
        }

        private void SpawnProcess()
        {
            Vector3 position = Vector3.zero;
            int randomIndex;

            for (int i = 0; i < _countOfEnemies; i++)
            {
                position += offenStep;
                randomIndex = Random.Range(0, _orkProperties.Length);
                SpawnEnemyBy(_orkProperties[randomIndex], position);

                position += offenStep;
                randomIndex = Random.Range(0, _elfProperties.Length);
                SpawnEnemyBy(_elfProperties[randomIndex], position);

                position += offenStep;
                randomIndex = Random.Range(0, _dragonProperties.Length);
                SpawnEnemyBy(_dragonProperties[randomIndex], position);
            }
        }

        private void SpawnEnemyBy(UnitProperty property, Vector3 position)
        {
            if (property is null)
                return;

            _spawner.Spawn(property, position);
        }
    }
}