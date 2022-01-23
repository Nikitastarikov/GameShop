using UnityEngine;

namespace GameShop
{
    public interface IProductModelInterface
    {
        delegate void ProductHandler(bool switcher);
        event ProductHandler ItemPurchased;
        public void SetProductOb(GameObject product);
        public int GetPrice();
        public void Purchase();
        public string GetName();
    }
}