using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace GameShop
{
    public class WalletView
    {
        Dictionary<string, CurrencyView> _currencyViewDictionary = new Dictionary<string, CurrencyView>();

        public Dictionary<string, CurrencyView> GetCurrencyViewList() => _currencyViewDictionary;
    }
}


