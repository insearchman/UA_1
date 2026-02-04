using System.Collections;
using UnityEngine;

namespace Modul_28_3
{
    [RequireComponent(typeof(KeyboardInput), typeof(UnitDestroyer))]
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _unitsGroup;
        [SerializeField] private Unit _unitPrefab;
        [SerializeField] private int _lifeTime = 3;

        private UnitDestroyer _unitDestroyer;
        private KeyboardInput _input;

        private void Awake()
        {
            _input = GetComponent<KeyboardInput>();
            _unitDestroyer = GetComponent<UnitDestroyer>();

            _input.ButtonPressed += SpawnUnit;
        }

        private void SpawnUnit(KeyboardComands keyboardComand)
        {
            Unit unit = Instantiate(_unitPrefab, _unitsGroup);

            switch (keyboardComand)
            {
                case KeyboardComands.SpawnUnit1:
                    unit.Kill();
                    _unitDestroyer.AddUnit(unit, KillByBool);
                    break;
                case KeyboardComands.SpawnUnit2:
                    _unitDestroyer.AddUnit(unit, KillByTime);
                    break;
                case KeyboardComands.SpawnUnit3:
                    _unitDestroyer.AddUnit(unit, KillByCapacity);
                    break;
                default:
                    break;
            }
        }

        private bool KillByBool(Unit unit)
        {
            return unit.IsDead;
        }

        private bool KillByTime(Unit unit)
        {
            StartCoroutine(LifeTime(unit));
            return unit.IsDead;
        }

        private bool KillByCapacity(Unit unit) => _unitDestroyer.IsOutOfCapacity();

        private IEnumerator LifeTime(Unit unit)
        {
            yield return new WaitForSeconds(_lifeTime);
            unit.Kill();
        }
    }
}