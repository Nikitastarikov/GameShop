using UnityEngine;
using UnityEngine.UI;

namespace GameShop
{
    public class BuyingForCoins : MonoBehaviour, IPurchaseMethodInterface
    {
        #region Fields
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Text _info;
        private string _name;
        #endregion

        #region PublicFunctions
        public BuyingForCoins()
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
            GameInstance.Instance.NotificationController.ShowNotification("Not enough " + _name);
            return false;
        }

        public Text GetPrice() => _info;
        public Button GetPurchaseButton() => _purchaseButton;
        public string GetNameCurrency() => _name;

        public void ConverterCurrency(int money)
        {
            _info.text = money.ToString();
        }

        public void OnDestroy()
        {
            _purchaseButton.onClick.RemoveAllListeners();
        }

        public void SetInfoPurchase(string name, int price)
        {
            _info.text = "The " + name + " consts " + price + " " + _name;
        }
        #endregion
    }
}
