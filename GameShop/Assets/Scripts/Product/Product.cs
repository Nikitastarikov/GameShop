using UnityEngine;

namespace GameShop
{
    public abstract class Product
    {
        #region Fields
        
        protected IProductModelInterface _productModel;
        protected IProductViewInterface _productView;

        #endregion

        #region PublicFunctions

        public void SetModel(IProductModelInterface model) => _productModel = model;

        public void Setview(IProductViewInterface view) => _productView = view;

        public IProductViewInterface GetView() => _productView;

        public IProductModelInterface GetModel() => _productModel;

        public abstract void CreateProductGameOb(GameObject productOb, Transform parent, Vector3 position);
        public abstract void CreateProductGameObUI(GameObject productObUI, Transform parent);
        #endregion
    }
}
