using UnityEngine;
using System.Collections.Generic;

namespace GameShop
{
    public class WalletController : MonoBehaviour
    {
        private Dictionary<string, int> _money;

        public WalletController()
        {
            _money = new Dictionary<string, int>();
        }

        public void AddMoney(string name, int amount)
        {
            if (!_money.ContainsKey(name))
                _money[name] = amount;
            else
                _money[name] += amount;
        }

        public void TakeAwayMoney(string name, int amount)
        {
            if (_money.ContainsKey(name))
            {
                _money[name] -= amount;
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
    }
}
