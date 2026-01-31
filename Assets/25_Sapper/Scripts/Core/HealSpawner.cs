using Modul_25.Gameplay;
using Modul_25.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Modul_25.Core
{
    public class HealSpawner
    {
        private const KeyCode ACTIVATE_KEY = KeyCode.F;
        private const int RANDOM_POINT_MAX_COUNT_TRY = 20;

        private Item _healItemPrefab;

        private int _healthHealing;
        private float _spawnDistance;
        private Transform _target;
        private NavMeshQueryFilter _filter;
        private NavMeshPath _path;

        private MonoBehaviour _monoBehaviour;
        private Coroutine _spawnCoroutine;
        private WaitForSeconds _delay;

        public HealSpawner(MonoBehaviour monoBehaviour, 
            Item healItemPrefab, 
            Transform target,
            NavMeshQueryFilter filter,
            int healthHealing, 
            float spawnCooldown, 
            float spawnDistance)
        {
            _monoBehaviour = monoBehaviour;
            _healItemPrefab = healItemPrefab;
            _target = target;
            _filter = filter;
            _healthHealing = healthHealing;
            _spawnDistance = spawnDistance;

            _delay = new WaitForSeconds(spawnCooldown);
            _path = new NavMeshPath();

            _spawnCoroutine = monoBehaviour.StartCoroutine(SpawnHeal());
        }

        public void Update()
        {
            ActivationSwitcher();
        }

        private void ActivationSwitcher()
        {
            if (!Input.GetKeyDown(ACTIVATE_KEY)) 
                return;

            if (_spawnCoroutine == null)
            {
                _spawnCoroutine = _monoBehaviour.StartCoroutine(SpawnHeal());
            }
            else
            {
                _monoBehaviour.StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        private IEnumerator SpawnHeal()
        {
            while (_target != null)
            {
                Vector3 position = GetRandomPoint();

                if (position != Vector3.zero)
                {
                    Item healItem = Object.Instantiate(_healItemPrefab, position, Quaternion.identity);
                    healItem.Init(_healthHealing);
                }

                yield return _delay;
            }

            _spawnCoroutine = null;
        }

        private Vector3 GetRandomPoint()
        {
            for (int i = 0; i < RANDOM_POINT_MAX_COUNT_TRY; i++)
            {
                Vector2 randomDir = Random.insideUnitCircle.normalized;
                Vector3 point = new Vector3(randomDir.x, 0, randomDir.y) * _spawnDistance + _target.position;

                if (NavMeshUtils.TryGetPath(_target.position, point, _filter, _path))
                    return point;
            }

            return Vector3.zero;
        }
    }
}