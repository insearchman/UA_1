using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modul_30_1_1
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private UICurrencyElement _currencyPrefab;
        [SerializeField] private Transform _elementsGroup;

        private WalletService _walletService;
        private Dictionary<CurrencyType, UICurrencyElement> _currenciesUI;

        private void Awake()
        {
            _currenciesUI = new();
            CreateUICurrency();
        }

        public void Init(WalletService walletService)
        {
            _walletService = walletService;
            _walletService.CurrencyValueChanged += OnCurrencyValueChanged;
        }

        private void OnCurrencyValueChanged(CurrencyType currencyType, int value)
        {
            string stringValue = value.ToString();

            UpdateValue(currencyType, stringValue);
        }

        private void UpdateValue(CurrencyType currencyType, string value)
        {
            _currenciesUI[currencyType].UpdateValue(value);
        }

        private void CreateUICurrency()
        {
            foreach (CurrencyType item in Enum.GetValues(typeof(CurrencyType)))
            {
                _currenciesUI.Add(item, Instantiate(_currencyPrefab, _elementsGroup));
                _currenciesUI[item].Init(item.ToString());
            }
        }

        private void OnDestroy()
        {
            _walletService.CurrencyValueChanged -= OnCurrencyValueChanged;
        }
    }
}