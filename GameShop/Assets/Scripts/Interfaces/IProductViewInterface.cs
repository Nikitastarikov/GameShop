using System.Collections.Generic;
using UnityEngine;

namespace GameShop
{
    public interface IProductViewInterface
    {
        public void CreatePurchaseMethods(List<GameObject> purchaseMethodsPrefabs, IProductModelInterface product);
        public void Show(IProductModelInterface product);
        public void SetProductObUI(GameObject productUI);
        public void SetActive(bool switcher);
    }
}
