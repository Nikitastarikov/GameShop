using System.Collections.Generic;
using UnityEngine;

namespace GameShop
{
    public class ShopController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<GameObject> _goodsPrefabs;
        private List<GameObject> _purchaseMethodsPrefab;
        private List<Product> _goods;
        private GameObject _productViewPrefab;
        private Transform _goodsBar;
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
            CreatePurchaseMethods();
            SelectProduct(0);
            
            foreach (var purchaseMethodOb in _purchaseMethodsPrefab)
            {
                if (purchaseMethodOb.TryGetComponent(out IPurchaseMethodInterface purchaseMethod))
                {
                    GameInstance.Instance.WalletController.AddMoney(purchaseMethod.GetNameCurrency(), 50);
                }
            }
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
            product.CreateProductGameObUI(_productViewPrefab, transform);
        }

        private void CreatePurchaseMethods()
        {
            foreach (var product in _goods)
            {
                product.GetView()
                    .CreatePurchaseMethods(_purchaseMethodsPrefab, product.GetModel());
            }
        }

        private void SelectProduct(int index)
        {
            for (int i = 0; i < _goods.Count; i++)
            {
                if (i != index)
                    _goods[i].GetView().
                        SetActive(false);
            }

            _goods[index].GetView().
                SetActive(true);
        }

        public void SetListMethodsPurchasePrefab(List<GameObject> listMethodsPurchasePrefab)
        {
            _purchaseMethodsPrefab = new List<GameObject>(listMethodsPurchasePrefab);
            
        }

        public void SetProductView(GameObject productViewPrefab)
        {
            _productViewPrefab = productViewPrefab;
        }

        public void SetTransformGoodsBar(Transform goodsBar)
        {
            _goodsBar = goodsBar;
        }
        
        public void IncrementCount()
        {
            _count++;
            SelectProduct(_count);
        }

        public void DecrementCount()
        {
            _count--;
            SelectProduct(_count);
        }
    }
}

