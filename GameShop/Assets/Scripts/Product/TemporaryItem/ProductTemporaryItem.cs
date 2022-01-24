using UnityEngine;

namespace GameShop
{
    public class ProductTemporaryItem : Product
    {
        public override void CreateProductGameOb(GameObject productOb, Transform parent, Vector3 position)
        {
            var item = MonoBehaviour.Instantiate(productOb, position, Quaternion.identity, parent);
            _productModel.SetProductOb(item);
        }

        public override void CreateProductGameObUI(GameObject productObUI, Transform parent)
        {
            var itemUI = MonoBehaviour.Instantiate(productObUI, parent);
            Setview(itemUI.GetComponent<IProductViewInterface>());
            _productView.SetProductObUI(itemUI);
        }
    }
}
