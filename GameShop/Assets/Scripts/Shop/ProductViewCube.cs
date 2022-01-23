using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameShop
{
    public class ProductViewCube : MonoBehaviour, IProductViewInterface
    {
        [SerializeField] private GameObject _isPurchase;
        [SerializeField] private Text _infoProduct;
        [SerializeField] private Transform _purchaseMethodsBar;
        private List<IPurchaseMethodInterface> _purchaseMethodsList;
        private GameObject _cubeObUI;

        public void CreatePurchaseMethods(List<GameObject> purchaseMethodsPrefabs, IProductModelInterface product)
        {
            _purchaseMethodsList = new List<IPurchaseMethodInterface>();
            foreach (var purchaseMethodOb in purchaseMethodsPrefabs)
            {
                if (purchaseMethodOb.TryGetComponent(out IPurchaseMethodInterface purchaseMethod))
                {
                    var productOb = Instantiate(purchaseMethodOb, _purchaseMethodsBar);
                    IPurchaseMethodInterface purchaseM = productOb.GetComponent<IPurchaseMethodInterface>();
                    _purchaseMethodsList.Add(purchaseM);

                    purchaseM.GetPurchaseButton().onClick.AddListener(() => purchaseM.Purchase(product));
                    product.ItemPurchased += delegate (bool switcher)
                    {
                        _isPurchase.SetActive(switcher);
                        _purchaseMethodsBar.gameObject.SetActive(!switcher);
                    };
                }
            }
        }

        /// <summary>
        /// Displaying objects
        /// </summary>
        public void Show(IProductModelInterface product)
        {
        }

        public void SetProductObUI(GameObject productUI) => _cubeObUI = productUI;

        public void SetActive(bool switcher)
        {
            _cubeObUI.SetActive(switcher);
        }
    }
}
