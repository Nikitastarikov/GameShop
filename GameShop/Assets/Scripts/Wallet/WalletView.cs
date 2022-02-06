using System.Collections.Generic;
using UnityEngine;

namespace GameShop
{
    /// <summary>
    /// Вьюха кошелька, отвечает за отображение кошелька
    /// </summary>
    public class WalletView : MonoBehaviour
    {
        private WalletController _walletController;

        [SerializeField]
        private GameObject _currencyViewPrefab;

        [SerializeField]
        private Transform _moneyBar;

        private Dictionary<string, CurrencyView> _currencyViewDictionary = new Dictionary<string, CurrencyView>();

        private void OnEnable()
        {
            _walletController = FindObjectOfType<WalletController>();

            if (_walletController)
            {
                _walletController.onChangedWallet += OnChangedWallet;
                _walletController.onAddCurrency += OnAddCurrency;
            }
        }

        private void OnDisable()
        {
            if (_walletController)
            {
                _walletController.onChangedWallet -= OnChangedWallet;
                _walletController.onAddCurrency -= OnAddCurrency;
            }
        }

        /// <summary>
        /// Устанавливает новые значение кошелька,
        /// когда запускается соответствующее событие
        /// </summary>
        /// <param name="name">Имя валюты</param>
        /// <param name="amount">Количество денег</param>
        public void OnChangedWallet(string name, int amount)
        {
            _currencyViewDictionary[name].Name.text = name;
            _currencyViewDictionary[name].Money.text = amount.ToString();
        }

        /// <summary>
        /// Добавление новой валюты
        /// </summary>
        /// <param name="name">Имя новой валюты</param>
        public void OnAddCurrency(string name)
        {
            var currencyView = Instantiate(_currencyViewPrefab, _moneyBar);

            if (currencyView.TryGetComponent(out CurrencyView viewCurrency))
            {
                if (!_currencyViewDictionary.ContainsKey(name))
                {
                    _currencyViewDictionary[name] = viewCurrency;
                }
            }
        }
    }
}


