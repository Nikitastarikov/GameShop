using UnityEngine;

namespace GameShop
{
    public class ProductModelCube : IProductModelInterface
    {
        #region Fields
        public GameObject _cubeOb;
        public int Price { get; private set; }
        public string Name { get; private set; }
        public event IProductModelInterface.ProductHandler ItemPurchased;

        #endregion

        public ProductModelCube(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public void SetProductOb(GameObject product)
        {
            _cubeOb = product;
        }

        public void Purchase()
        {
            ItemPurchased?.Invoke(true);
        }
    }
}
