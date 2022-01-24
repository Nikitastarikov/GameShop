using UnityEngine;

namespace GameShop
{
    public interface IProductModelInterface
    {
        delegate void ProductHandler(IProductModelInterface product);
        
        event ProductHandler ItemPurchased;

        public void SetProductOb(GameObject product);
        public int GetPrice();
        public bool GetIsPurchase();
        public string GetName();
        public string GetInfo();
        public void Purchase();

        public void SetIsPurchase(bool switcher);
    }
}