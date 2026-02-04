using System;
using System.Collections.Generic;

namespace Modul_28_1
{
    public class WalletService
    {
        public event Action<CurrencyType, int> CurrencyValueChanged;

        private ICurrencyChanger _currencyChanger;

        private readonly Dictionary<CurrencyType, Currency> _wallet = new();

        public WalletService(ICurrencyChanger currencyChanger)
        {
            _currencyChanger = currencyChanger;
            if (_currencyChanger != null)
                _currencyChanger.CurrencyChangeButtonPressed += OnCurrencyChangeButtonPressed;

            FillWallet();
        }

        public void OnDestroy()
        {
            if (_currencyChanger != null)
                _currencyChanger.CurrencyChangeButtonPressed -= OnCurrencyChangeButtonPressed;

            foreach (var currency in _wallet.Values)
                currency.ValueChanged -= OnValueChanged;
        }

        private void FillWallet()
        {
            foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
            {
                _wallet[type] = new Currency(type);
                _wallet[type].ValueChanged += OnValueChanged;
            }
        }

        private void OnValueChanged(CurrencyType currencyType, int value)
        {
            CurrencyValueChanged?.Invoke(currencyType, value);
        }

        private void OnCurrencyChangeButtonPressed(CurrencyType currencyType, CurrencyOperation currencyOperation, int value)
        {
            switch (currencyOperation)
            {
                case CurrencyOperation.Add:
                    _wallet[currencyType].Add(value);
                    break;
                case CurrencyOperation.Spend:
                    _wallet[currencyType].Spend(value);
                    break;
                default:
                    break;
            }
        }
    }
}