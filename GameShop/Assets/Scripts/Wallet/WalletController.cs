using UnityEngine;
using System.Collections.Generic;

namespace GameShop
{
    public class WalletController : MonoBehaviour
    {
        private GameObject _currencyViewPrefab;
        private Transform _moneyBar;
        private WalletView _walletView = new WalletView();
        private Dictionary<string, int> _money;

        public WalletController()
        {
            _money = new Dictionary<string, int>();
        }

        public void AddCurrency(string name, int amount)
        {
            if (!_money.ContainsKey(name))
                _money[name] = amount;
            else
                _money[name] += amount;

            var currencyView = Instantiate(_currencyViewPrefab, _moneyBar);

            if (currencyView.TryGetComponent(out CurrencyView view))
            {
                _walletView.GetCurrencyViewList()[name] = view;
            }
        }

        public void AddMoney(string name, int amount)
        {
            if (!_money.ContainsKey(name))
                _money[name] = amount;
            else
                _money[name] += amount;

            _walletView.GetCurrencyViewList()[name].SetMoney(_money[name]);

            SavingData();
        }

        public void TakeAwayMoney(string name, int amount)
        {
            if (_money.ContainsKey(name))
            {
                _money[name] -= amount;
                _walletView.GetCurrencyViewList()[name].SetMoney(_money[name]);
                SavingData();
                Debug.Log(name + " = " + _money[name]);
            }
            else
                Debug.Log("This currency has not been added");
        }

        public int GetMoney(string name)
        {
            if (_money.ContainsKey(name))
                return _money[name];
            return -1;
        }

        public void SetWalletView(WalletView walletView)
        {
            _walletView = walletView;
        }

        public void SetMoneyBar(GameObject currencyViewPrefab, Transform moneyBar)
        {
            _currencyViewPrefab = currencyViewPrefab;
            _moneyBar = moneyBar;
        }

        [ContextMenu("LoadingWalletData")]
        public void LoadingData()
        {
            string[] arrayStr = new string[_money.Count];
            int i = 0;
            foreach (var money in _money)
            {
                arrayStr[i] = money.Key;
                i++;
            }

            for (i = 0; i < _money.Count; i++)
            {
                _money[arrayStr[i]] = StorageControllerAntyhack.GetInt(arrayStr[i], 100);
                _walletView.GetCurrencyViewList()[arrayStr[i]].SetMoney(_money[arrayStr[i]]);
                _walletView.GetCurrencyViewList()[arrayStr[i]].SetName(arrayStr[i]);
                Debug.Log(arrayStr[i] + " " + _money[arrayStr[i]]);
            }
        }

        [ContextMenu("SavingWalletData")]
        public void SavingData()
        {
            foreach (var money in _money)
            {
                StorageControllerAntyhack.SetInt(money.Key, money.Value);
                Debug.Log("Save " + money.Key + " = " + money.Value);
            }
        }
    }
}
