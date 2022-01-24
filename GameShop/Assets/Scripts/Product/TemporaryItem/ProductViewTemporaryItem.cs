using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace GameShop
{
    public class ProductViewTemporaryItem : MonoBehaviour, IProductViewInterface
    {
        [SerializeField] private GameObject _isPurchase;
        [SerializeField] private Text _infoProduct;
        [SerializeField] private Transform _purchaseMethodsBar;
        private List<IPurchaseMethodInterface> _purchaseMethodsList;
        private GameObject _obUI;

        public void CreatePurchaseMethods(List<GameObject> purchaseMethodsPrefabs, IProductModelInterface product)
        {
            if (!product.GetIsPurchase())
            {
                _purchaseMethodsList = new List<IPurchaseMethodInterface>();
                foreach (var purchaseMethodOb in purchaseMethodsPrefabs)
                {
                    if (purchaseMethodOb.TryGetComponent(out IPurchaseMethodInterface purchaseMethod))
                    {
                        var productOb = Instantiate(purchaseMethodOb, _purchaseMethodsBar);
                        IPurchaseMethodInterface purchaseM = productOb.GetComponent<IPurchaseMethodInterface>();
                        purchaseM.SetInfoPurchase(product.GetName(), product.GetPrice());
                        _purchaseMethodsList.Add(purchaseM);

                        purchaseM.GetPurchaseButton().onClick.AddListener(() => purchaseM.Purchase(product));
                        product.ItemPurchased += delegate (IProductModelInterface product)
                        {
                            _isPurchase.SetActive(true);
                            _purchaseMethodsBar.gameObject.SetActive(false);
                            Show(product);
                        };
                    }
                }
            }
            else
            {
                _isPurchase.SetActive(true);
                _purchaseMethodsBar.gameObject.SetActive(false);
                Show(product);
            }
        }

        /// <summary>
        /// Displaying objects
        /// </summary>
        public void Show(IProductModelInterface product)
        {
            StartCoroutine(CoroutineShow(product));
        }

        private IEnumerator CoroutineShow(IProductModelInterface product)
        {
            ShowInfoTemporaryItem(product);

            yield return new WaitForSeconds(60);

            ShowInfoTemporaryItem(product);
        }

        private void ShowInfoTemporaryItem(IProductModelInterface product)
        {
            if (product.GetIsPurchase())
            {
                _infoProduct.text = product.GetInfo();
            }
            else
            {
                _isPurchase.SetActive(false);
                _purchaseMethodsBar.gameObject.SetActive(true);
            }
        }

        public void SetProductObUI(GameObject productUI) => _obUI = productUI;

        public void SetActive(bool switcher)
        {
            _obUI.SetActive(switcher);
        }
    }
}
