using UnityEngine;
using Zenject;
using GameShop;
using System.Collections.Generic;

public class MainInstaller : MonoInstaller
{
    public List<GameObject> _listMethodsPurchasePrefabs;
    public CameraController CameraController;
    public GameObject ProductViewPrefab;
    public GameObject Shop;
    public GameObject GameInstanceOb;
    public Transform Canvas;
    public Transform GoodsBar;

    public override void InstallBindings()
    {
        BindShop();
        BindStorageController();
        BindWalletController();
        BindCameraController();
    }

    private void BindCameraController()
    {
        Container
            .BindInstance<CameraController>(CameraController)
            .AsSingle();
    }

    private void BindWalletController()
    {
        WalletController walletController = GameInstanceOb.GetComponent<WalletController>();

        Container
            .Bind<WalletController>()
            .FromInstance(walletController)
            .AsSingle();
    }

    private void BindStorageController()
    {
        StorageController strorageController = GameInstanceOb.GetComponent<StorageController>();

        Container
            .Bind<StorageController>()
            .FromInstance(strorageController)
            .AsSingle();
    }

    private void BindShop()
    {
        ShopController shopController = Container
            .InstantiatePrefabForComponent<ShopController>(Shop, Canvas);

        shopController.SetProductView(ProductViewPrefab);
        shopController.SetListMethodsPurchasePrefab(_listMethodsPurchasePrefabs);
        shopController.SetTransformGoodsBar(GoodsBar);

        Container
            .Bind<ShopController>()
            .FromInstance(shopController)
            .AsSingle();
    }
}