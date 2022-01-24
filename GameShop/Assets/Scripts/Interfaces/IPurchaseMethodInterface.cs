using UnityEngine.UI;

namespace GameShop
{
    public interface IPurchaseMethodInterface
    {
        public bool Purchase(IProductModelInterface product);
        public string GetNameCurrency();
        public void ConverterCurrency(int money);

        public Text GetPrice();
        public void SetInfoPurchase(string name, int price);

        public Button GetPurchaseButton();
    }
}
