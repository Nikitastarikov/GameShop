using UnityEngine;
using Zenject;
using GameShop;

public class MainInstaller : MonoInstaller
{
    public GameObject ShopController;
    public GameObject StorageControllerOb;
    public Transform Canvas;
    public Transform GoodsBar;

    public override void InstallBindings()
    {
        BindShopController();
        BindStorageController();
    }

    private void BindStorageController()
    {
        StorageController storageController = Container
                    .InstantiatePrefabForComponent<StorageController>(StorageControllerOb);

        Container
            .Bind<StorageController>()
            .FromInstance(storageController)
            .AsSingle();
    }

    private void BindShopController()
    {
        ShopController shopController = Container
            .InstantiatePrefabForComponent<ShopController>(ShopController, Canvas);

        shopController.SetTransformGoodsBar(GoodsBar);

        Container
            .Bind<ShopController>()
            .FromInstance(shopController)
            .AsSingle();
    }
}