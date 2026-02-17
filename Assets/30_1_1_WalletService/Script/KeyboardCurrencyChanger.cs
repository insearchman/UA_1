using System;
using UnityEngine;

namespace Modul_30_1_1
{
    public class KeyboardCurrencyChanger : MonoBehaviour, ICurrencyChanger
    {
        public event Action<CurrencyType, CurrencyOperation, int> CurrencyChangeButtonPressed;

        [SerializeField] private int _currencyChangeValue = 1;

        [Header("Button binding")]
        [SerializeField] private KeyCode _coinAddKey = KeyCode.Q;
        [SerializeField] private KeyCode _coinSpendKey = KeyCode.A;
        [SerializeField] private KeyCode _diamondAddKey = KeyCode.W;
        [SerializeField] private KeyCode _diamondSpendKey = KeyCode.S;
        [SerializeField] private KeyCode _energyAddKey = KeyCode.E;
        [SerializeField] private KeyCode _energySpendKey = KeyCode.D;

        void Update()
        {
            InputHandler();
        }

        private void InputHandler()
        {
            if (Input.GetKeyDown(_coinAddKey))
                CurrencyChangeButtonPressed?.Invoke(CurrencyType.Coin, CurrencyOperation.Add, _currencyChangeValue);
            if (Input.GetKeyDown(_coinSpendKey))
                CurrencyChangeButtonPressed?.Invoke(CurrencyType.Coin, CurrencyOperation.Spend, _currencyChangeValue);
            if (Input.GetKeyDown(_diamondAddKey))
                CurrencyChangeButtonPressed?.Invoke(CurrencyType.Diamond, CurrencyOperation.Add, _currencyChangeValue);
            if (Input.GetKeyDown(_diamondSpendKey))
                CurrencyChangeButtonPressed?.Invoke(CurrencyType.Diamond, CurrencyOperation.Spend, _currencyChangeValue);
            if (Input.GetKeyDown(_energyAddKey))
                CurrencyChangeButtonPressed?.Invoke(CurrencyType.Energy, CurrencyOperation.Add, _currencyChangeValue);
            if (Input.GetKeyDown(_energySpendKey))
                CurrencyChangeButtonPressed?.Invoke(CurrencyType.Energy, CurrencyOperation.Spend, _currencyChangeValue);
        }
    }
}