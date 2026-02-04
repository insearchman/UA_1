using System;

namespace Modul_28_1
{
    public interface ICurrencyChanger
    {
        event Action<CurrencyType, CurrencyOperation, int> CurrencyChangeButtonPressed;
    }
}