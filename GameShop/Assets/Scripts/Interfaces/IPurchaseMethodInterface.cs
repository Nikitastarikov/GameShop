using UnityEngine.UI;

namespace GameShop
{
    public interface IPurchaseMethodInterface
    {
        public bool Purchase(IProductModelInterface product);
        public string GetNameCurrency();
        public void ConverterCurrency(int money);

        public Button GetPurchaseButton();
        public void UnsubscribeAll();
    }
}
