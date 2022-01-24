using UnityEngine;
using System;

namespace GameShop
{
    public class ProductModelTemporaryItem : IProductModelInterface
    {
        #region Fields
        private GameObject _ob;
        private int _secEndDate;
        private int _remainingTime;
        private int _timeMin;
        private string _info;
        private string _name;
        private int _price;
        private bool _isPurchase;
        public event IProductModelInterface.ProductHandler ItemPurchased;
        #endregion

        public ProductModelTemporaryItem(string name, int price, int timeMin)
        {
            _timeMin = timeMin;
            _info = "Temporary object, remaining time: ";
            _name = name;
            _price = price;
            LoadingData();
        }

        public bool GetIsPurchase() => _isPurchase;

        public int GetPrice() => _price;

        public string GetName() => _name;

        public string GetInfo()
        {
            DateTime currentDate = DateTime.UtcNow.ToLocalTime();
            int sec = ConvertDateTimeToInt32(currentDate);
            _remainingTime = _secEndDate - sec;
            int hours = _remainingTime / 3600;
            int minutes = _remainingTime % 3600 / 60;
            sec = _remainingTime % 3600 % 60;
            Debug.Log(_info + " " + hours + ":" + minutes + ":" + sec);
            return _info + " " + hours + ":" + minutes + ":" + sec;
        }
        public void Purchase()
        {
            _isPurchase = true;
            SavingData();
            ItemPurchased.Invoke(this);
        }

        public void SetProductOb(GameObject product) => _ob = product;
        public void SetIsPurchase(bool switcher) => _isPurchase = switcher;

        private void LoadingData()
        {
            _isPurchase = StorageControllerAntyhack.GetInt(_name, 0) == 1;

            if (_isPurchase)
            {
                int secEndDate = StorageControllerAntyhack.GetInt(_name + "EndTime", 0);
                _secEndDate = secEndDate;
                DateTime currentDate = DateTime.UtcNow.ToLocalTime();

                if (secEndDate == 0)
                {
                    _isPurchase = false;
                    return;
                }
                int sec = ConvertDateTimeToInt32(currentDate);
                secEndDate -= sec;
                _remainingTime = secEndDate / 60;
            }
        }

        private void SavingData()
        {
            int value = _isPurchase == true ? 1 : 0;
            StorageControllerAntyhack.SetInt(_name, value);

            int sec;
            DateTime startDate = DateTime.UtcNow.ToLocalTime();
            sec = ConvertDateTimeToInt32(startDate);
            sec += _timeMin * 60;
            _secEndDate = sec;
            StorageControllerAntyhack.SetInt(_name + "EndTime", sec);
        }

        public static int ConvertDateTimeToInt32(DateTime dt)
        {
            return (int)(dt - new DateTime(2017, 1, 1)).TotalSeconds;
        }

        public static DateTime ConvertInt32ToDateTime(int i)
        {
            return new DateTime(2017, 1, 1).AddSeconds(i);
        }
    }
}
