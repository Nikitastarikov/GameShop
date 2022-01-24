using System.Collections.Generic;
using UnityEngine;

namespace GameShop
{
    public class ShopController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<GameObject> _goodsPrefabs;
        private List<GameObject> _purchaseMethodsPrefab;
        private List<GameObject> _productViewPrefabList;
        private List<Product> _goods;
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
            TemporaryItem();
            CreatePurchaseMethods();
            SelectProduct(0);
            
            foreach (var purchaseMethodOb in _purchaseMethodsPrefab)
            {
                if (purchaseMethodOb.TryGetComponent(out IPurchaseMethodInterface purchaseMethod))
                {
                    GameInstance.Instance.WalletController.AddCurrency(purchaseMethod.GetNameCurrency(), 100);
                }
            }
            GameInstance.Instance.WalletController.LoadingData();
        }

        private void TemporaryItem()
        {
            FactoryTemporaryItem factoryTemporaryItem = new FactoryTemporaryItem();

            ProductTemporaryItem item = new ProductTemporaryItem();

            ProductionFacilities(item, factoryTemporaryItem, _goodsPrefabs[2], "Stars", 60
                , new Vector3(_startPos.x + _distanceBetweenGoods * 2, _startPos.y, _startPos.z), 1, 60);

            _goods.Add(item);
        }

        private void ProductionCubes()
        {
            FactoryCube factoryCube = new FactoryCube();

            ProductCube cube = new ProductCube();
            ProductCube cubeTwo = new ProductCube();

            ProductionFacilities(cube, factoryCube, _goodsPrefabs[0], "RedCube", 50, _startPos, 0);
            ProductionFacilities(cubeTwo, factoryCube, _goodsPrefabs[1], "BlueCube", 50
                , new Vector3(_startPos.x + _distanceBetweenGoods, _startPos.y, _startPos.z), 0);

            _goods.Add(cube);
            _goods.Add(cubeTwo);
        }

        private void ProductionFacilities(Product product, IFactoryInterface factory
            , GameObject productOb, string name
            , int price, Vector3 pos, int indexView, int timeMin = 0)
        {
            IProductModelInterface productModel = factory.FactoryMethod(name, price, timeMin);
            product.SetModel(productModel);
            product.CreateProductGameOb(productOb, _goodsBar, pos);
            product.CreateProductGameObUI(_productViewPrefabList[indexView], transform);
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

        public void SetProductView(List<GameObject> productViewPrefabList)
        {
            _productViewPrefabList = new List<GameObject>(productViewPrefabList);
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

