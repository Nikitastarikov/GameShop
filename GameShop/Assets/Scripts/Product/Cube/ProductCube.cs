using UnityEngine;

namespace GameShop
{
    public class ProductCube : Product
    {
        public override void CreateProductGameOb(GameObject productOb, Transform parent, Vector3 position)
        {
            var cube = MonoBehaviour.Instantiate(productOb, position, Quaternion.identity, parent);
            _productModel.SetProductOb(cube);
        }
    }
}
