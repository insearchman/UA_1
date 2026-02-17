using System;

namespace Modul_30_1_1
{
    public interface ICurrencyChanger
    {
        event Action<CurrencyType, CurrencyOperation, int> CurrencyChangeButtonPressed;
    }
}