using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace GameShop
{
    /// <summary>
    /// Контроллер кошелька, отвечает за манипуляции
    /// над деньгами
    /// </summary>
    public class WalletController : MonoBehaviour
    {
        public event Action<string> onAddCurrency = delegate { };
        public event Action<string, int> onChangedWallet = delegate { };

        private Dictionary<string, int> _money = new Dictionary<string, int>();
        public IReadOnlyDictionary<string, int> Money => _money;       
        
        /// <summary>
        /// Запускает событие изменения значений кошелька
        /// </summary>
        /// <param name="name">Имя валюты</param>
        /// <param name="amount">Количество денег</param>
        public void OnChangeWallet(string name, int amount)
        {
            onChangedWallet(name, amount);
        }

        /// <summary>
        /// Добавление новой валюты
        /// </summary>
        /// <param name="name">Имя валюты</param>
        /// <param name="amount">Количество денег</param>
        public void AddCurrency(string name, int amount)
        {
            _money[name] = amount;

            onAddCurrency(name);
            OnChangeWallet(name, _money[name]);
        }

        /// <summary>
        /// Добавление денег для определенной валюты
        /// </summary>
        /// <param name="name">Имя валюты</param>
        /// <param name="amount">Количество денег</param>
        public void AddMoney(string name, int amount)
        {
            if (_money.ContainsKey(name))
            {
                _money[name] += amount;
                OnChangeWallet(name, _money[name]);
                SavingData();
            }
        }

        /// <summary>
        /// Вычитание денег для опеределнной валюты
        /// </summary>
        /// <param name="name">Имя валюты</param>
        /// <param name="amount">Колиичество денег</param>
        public void TakeAwayMoney(string name, int amount)
        {
            if (_money.ContainsKey(name))
            {
                _money[name] -= amount;
                GameInstance.Instance.NotificationController.ShowNotification("Successful purchase");
                OnChangeWallet(name, _money[name]);
                SavingData();
            }
            else
            {
                GameInstance.Instance.NotificationController.ShowNotification("Not enough " + name);
            }
        }

        /// <summary>
        /// Возвращает количество денег определенной валюты 
        /// </summary>
        /// <param name="name">Имя валюты</param>
        /// <returns>Количества денег или -1, если нет искомой валюты</returns>
        public int GetMoney(string name)
        {
            if (_money.ContainsKey(name))
            {
                return _money[name];
            }  
            
            return -1;
        }

        /// <summary>
        /// Загрузка данных кошелька
        /// </summary>
        public void LoadingData()
        {
            _money.ToList().ForEach(kv =>
            {
                _money[kv.Key] = StorageControllerAntyhack.GetInt(kv.Key, 100);
                OnChangeWallet(kv.Key, _money[kv.Key]);
            });
        }

        /// <summary>
        /// Сохранение данных кошелька
        /// </summary>
        public void SavingData()
        {
            foreach (var money in _money)
            {
                StorageControllerAntyhack.SetInt(money.Key, money.Value);                
            }
        }
    }
}
