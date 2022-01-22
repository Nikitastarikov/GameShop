using UnityEngine;

namespace GameShop
{
    public abstract class Product
    {
        #region Fields
        
        protected IProductModelInterface _productModel;

        #endregion

        #region PublicFunctions

        public void SetModel(IProductModelInterface model)
        {
            _productModel = model; 
        }

        public abstract void CreateProductGameOb(GameObject productOb, Transform parent, Vector3 position);
        #endregion
    }
}
