using UnityEngine;

namespace GameShop
{
    public class ProductModelCube : IProductModelInterface
    {
        #region Fields
        private GameObject _cubeOb;
        private string _info;
        private string _name;
        private int _price;
        private bool _isPurchase;
        public event IProductModelInterface.ProductHandler ItemPurchased;
        #endregion

        #region PublicFunctions
        public ProductModelCube(string name, int price)
        {
            _info = "Обычный куб";
            _name = name;
            _price = price;
            LoadingData();
        }

        public void SetProductOb(GameObject product) => _cubeOb = product;
        public void SetIsPurchase(bool switcher) => _isPurchase = switcher;

        public bool GetIsPurchase() => _isPurchase;

        public int GetPrice() => _price;

        public string GetName() => _name;

        public string GetInfo() => _info;

        public void Purchase()
        {
            _isPurchase = true;
            SavingData();
            ItemPurchased.Invoke(this);
        }
        #endregion

        #region PubclicFunctions
        private void LoadingData()
        {
            _isPurchase = StorageControllerAntyhack.GetInt(_name, 0) == 1;
        }

        private void SavingData()
        {
            int value = _isPurchase == true ? 1 : 0;
            StorageControllerAntyhack.SetInt(_name, value);
        }
        #endregion
    }
}
