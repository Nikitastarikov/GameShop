using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameShop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private Text _isPurchase;
        [SerializeField] private Transform _purchaseMethodsBar;
        [SerializeField] private Transform _goodsBar;

        /// <summary>
        /// Displaying objects
        /// </summary>
        public void Show(List<IProductModelInterface> _goods, int count)
        {
            
        }
    }
}
