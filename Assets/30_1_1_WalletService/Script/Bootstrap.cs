using UnityEngine;

namespace Modul_30_1_1
{
    public enum CurrencyType
    {
        Coin,
        Diamond,
        Energy
    }

    public enum CurrencyOperation
    {
        Add,
        Spend
    }

    [RequireComponent(typeof(WalletView), typeof(KeyboardCurrencyChanger))]
    public class Bootstrap : MonoBehaviour
    {
        private WalletView _uiView;
        private KeyboardCurrencyChanger _currencyChanger;

        private WalletService _walletService;

        private void Awake()
        {
            _uiView = GetComponent<WalletView>();
            _currencyChanger = GetComponent<KeyboardCurrencyChanger>();

            _walletService = new(_currencyChanger);

            _uiView.Init(_walletService);
        }

        private void OnDestroy()
        {
            _walletService.OnDestroy();
        }
    }
}