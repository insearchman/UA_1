using UnityEngine;
using UnityEngine.AI;

using Modul_23.Gameplay;

namespace Modul_23.Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LayerMask _floorMask;

        [SerializeField] private Character _player;
        [SerializeField] private Transform _pointFlagPrefab;
        [SerializeField] private float _timeToBored = 5f;

        private ActiveIdleControllersManager _playerBehaviourController;

        private void Awake()
        {
            NavMeshQueryFilter filter = GetFilter();

            Transform pointFlag = Instantiate(_pointFlagPrefab);

            _playerBehaviourController = new ActiveIdleControllersManager(_player, 
                new PointCharacterController(_player, _floorMask, filter, pointFlag, _timeToBored), 
                new RandomCharacterController(_player, filter));

            _playerBehaviourController.Enable();
        }

        private void Update()
        {
            _playerBehaviourController.Update(Time.deltaTime);
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