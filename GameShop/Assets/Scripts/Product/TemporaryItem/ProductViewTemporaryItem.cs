using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace GameShop
{
    public class ProductViewTemporaryItem : MonoBehaviour, IProductViewInterface
    {
        #region Fields
        [SerializeField] private GameObject _isPurchase;
        [SerializeField] private Text _infoProduct;
        [SerializeField] private Transform _purchaseMethodsBar;
        private List<IPurchaseMethodInterface> _purchaseMethodsList;
        private IProductModelInterface _product;
        private GameObject _obUI;
        #endregion

        #region PublicFunctions
        public void CreatePurchaseMethods(List<GameObject> purchaseMethodsPrefabs, IProductModelInterface product)
        {
            _product = product;
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

        public IEnumerator CoroutineShow(IProductModelInterface product)
        {
            while (product.GetIsPurchase())
            {       
                _infoProduct.text = product.GetInfo();
                yield return new WaitForSeconds(1);
            }

            _isPurchase.SetActive(false);
            _purchaseMethodsBar.gameObject.SetActive(true);
        }

        public void SetProductObUI(GameObject productUI) => _obUI = productUI;

        public void SetActive(bool switcher)
        {
            _obUI.SetActive(switcher);
        }
        #endregion

        #region PrivateFunctions
        private void OnEnable()
        {
            if (_product != null)
            {
                _isPurchase.SetActive(true);
                _purchaseMethodsBar.gameObject.SetActive(false);
                Show(_product);
            }
        }
        #endregion
    }
}
