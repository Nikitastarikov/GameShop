using UnityEngine;
using Zenject;
using GameShop;
using System.Collections.Generic;

public class MainInstaller : MonoInstaller
{
    public List<GameObject> MethodsPurchasePrefabsList;
    public List<GameObject> ProductViewPrefabList;
    public CameraController CameraController;
    public GameObject NotificationBar;
    public GameObject CurrencyViewPrefab;
    public GameObject Shop;
    public GameObject GameInstanceOb;
    public Transform Canvas;
    public Transform MoneyBar;
    public Transform GoodsBar;

    public override void InstallBindings()
    {
        BindShop();
        BindStorageController();
        BindWalletController();
        BindCameraController();
        BindNotificationController();
    }

    private void BindNotificationController()
    {
        NotificationController notificationController = new NotificationController();
        notificationController.SetNotificationBar(NotificationBar);

        Container
            .Bind<NotificationController>()
            .FromInstance(notificationController)
            .AsSingle();
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

        walletController.SetMoneyBar(CurrencyViewPrefab, MoneyBar);

        Container
            .Bind<WalletController>()
            .FromInstance(walletController)
            .AsSingle();
    }

    private void BindStorageController()
    {
        StorageControllerAntyhack strorageController = GameInstanceOb.GetComponent<StorageControllerAntyhack>();

        Container
            .Bind<StorageControllerAntyhack>()
            .FromInstance(strorageController)
            .AsSingle();
    }

    private void BindShop()
    {
        ShopController shopController = Container
            .InstantiatePrefabForComponent<ShopController>(Shop, Canvas);

        shopController.SetProductView(ProductViewPrefabList);
        shopController.SetListMethodsPurchasePrefab(MethodsPurchasePrefabsList);
        shopController.SetTransformGoodsBar(GoodsBar);

        Container
            .Bind<ShopController>()
            .FromInstance(shopController)
            .AsSingle();
    }
}