using UnityEngine;
using UnityEngine.AI;

using Modul_25.Gameplay;

namespace Modul_25.Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LayerMask _floorMask;

        [SerializeField] private Character _player;
        [SerializeField] private Transform _pointFlagPrefab;
        [SerializeField] private float _timeToBored = 5f;

        private ActiveIdleControllersManager _playerBehaviourController;

        private HealSpawner _healSpawner;
        [SerializeField] private Item _healingBottlePrefab;
        [SerializeField] private int _healthHealing = 10;
        [SerializeField] private float _spawnCooldown = 3f;
        [SerializeField] private float _spawnDistance = 3f;

        private void Awake()
        {
            NavMeshQueryFilter filter = GetFilter();

            Transform pointFlag = Instantiate(_pointFlagPrefab);
            pointFlag.gameObject.SetActive(false);

            _playerBehaviourController = new ActiveIdleControllersManager(_player, 
                new PointCharacterController(_player, _floorMask, filter, pointFlag, _timeToBored), 
                new RandomCharacterController(_player, filter));

            _playerBehaviourController.Enable();

            _healSpawner = new HealSpawner(this, _healingBottlePrefab, _player.transform, filter, _healthHealing, _spawnCooldown, _spawnDistance);
        }

        private void Update()
        {
            _playerBehaviourController.Update(Time.deltaTime);
            _healSpawner.Update();
        }

        private NavMeshQueryFilter GetFilter()
        {
            return new NavMeshQueryFilter()
            {
                agentTypeID = 0,
                areaMask = NavMesh.AllAreas
            };
        }
    }
}