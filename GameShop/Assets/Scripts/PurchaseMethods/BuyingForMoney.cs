using UnityEngine;
using UnityEngine.UI;

namespace GameShop
{
    public class BuyingForMoney : MonoBehaviour, IPurchaseMethodInterface
    {
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Text _price;
        private string _name;

        public BuyingForMoney()
        {
            _name = "Coins";
        }
        
        public bool Purchase(IProductModelInterface product)
        {
            int _money = GameInstance.Instance.WalletController.GetMoney(_name);
            if (_money >= product.GetPrice())
            {
                product.Purchase();
                // Making a purchase and take away the price of the goods
                GameInstance.Instance.WalletController.TakeAwayMoney(_name, product.GetPrice());
                return true;
            }
            return false;
        }

        public Button GetPurchaseButton() => _purchaseButton;
        public string GetNameCurrency() => _name;

        public void ConverterCurrency(int money)
        {
            _price.text = money.ToString();
        }

        public void UnsubscribeAll()
        {
            _purchaseButton.onClick.RemoveAllListeners();
        }
    }
}
