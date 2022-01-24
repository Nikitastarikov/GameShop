using UnityEngine;
using System.Collections.Generic;

namespace GameShop
{
    public class EarningsController : MonoBehaviour
    {
        public void GoToWork()
        {
            string[] arrayStr = GetCurrencyName();
            for (int i = 0; i < arrayStr.Length; i++)
            {
                GameInstance.Instance.WalletController.AddMoney(arrayStr[i], 100);
            }
        }

        public void AskForALoan()
        {
            int rand = Random.Range(0, 100);
            if (0 <= rand && rand < 30)
            {
                string[] arrayStr = GetCurrencyName();
                for (int i = 0; i < arrayStr.Length; i++)
                {
                    GameInstance.Instance.WalletController.AddMoney(arrayStr[i], 30);
                }
                Debug.Log("Luck!");
            }
            else
                Debug.Log("Unluck!");
        }

        public string[] GetCurrencyName()
        {
            Dictionary<string, int> money = GameInstance.Instance.WalletController.Money;
            string[] arrayStr = new string[money.Count];
            int i = 0;
            foreach (var currency in money)
            {
                arrayStr[i] = currency.Key;
                i++;
            }
            return arrayStr;
        }
    }
}
