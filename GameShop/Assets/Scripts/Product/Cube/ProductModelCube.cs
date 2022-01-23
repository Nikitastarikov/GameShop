using UnityEngine;

namespace GameShop
{
    public class ProductModelCube : IProductModelInterface
    {
        #region Fields
        private GameObject _cubeOb;
        private string _name;
        private int _price;
        public event IProductModelInterface.ProductHandler ItemPurchased;
        #endregion

        public ProductModelCube(string name, int price)
        {
            _name = name;
            _price = price;
        }

        public void SetProductOb(GameObject product) => _cubeOb = product;

        public int GetPrice() => _price;

        public string GetName() => _name;

        public void Purchase()
        {
            ItemPurchased.Invoke(true);
        }
    }
}
