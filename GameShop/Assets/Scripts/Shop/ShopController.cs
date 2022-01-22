using System.Collections.Generic;
using UnityEngine;

namespace GameShop
{
    public class ShopController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<GameObject> _goodsPrefabs;
        private Transform _goodsBar;
        private List<Product> _goods;
        private ShopView _shopView;
        private Vector3 _startPos;
        private float _distanceBetweenGoods;
        private int _count;

        public int Count => _count;
        public List<Product> Goods => _goods;
        public float DistanceBetweenGoods => _distanceBetweenGoods;

        #endregion

        private void Start()
        {
            _count = 0;
            _goods = new List<Product>();
            _startPos = new Vector3(0f, 0f, 0f);
            _distanceBetweenGoods = 10f;

            ProductionCubes();
        }

        private void ProductionCubes()
        {
            FactoryCube factoryCube = new FactoryCube();

            ProductCube cube = new ProductCube();
            ProductCube cubeTwo = new ProductCube();
            ProductionFacilities(cube, factoryCube, _goodsPrefabs[0], "RedCube", 50, _startPos);
            ProductionFacilities(cubeTwo, factoryCube, _goodsPrefabs[1], "BlueCube", 50, new Vector3(_startPos.x + _distanceBetweenGoods, _startPos.y, _startPos.z));
            _goods.Add(cube);
            _goods.Add(cubeTwo);
        }

        private void ProductionFacilities(Product product, IFactoryInterface factory, GameObject productOb, string name, int price, Vector3 pos)
        {
            IProductModelInterface productModel = factory.FactoryMethod(name, price);
            product.SetModel(productModel);
            product.CreateProductGameOb(productOb, _goodsBar, pos);
        }

        public void SetTransformGoodsBar(Transform goodsBar)
        {
            _goodsBar = goodsBar;
        }
        
        public void IncrementCount()
        {
            _count++;
        }

        public void DecrementCount()
        {
            _count--;
        }
    }
}

