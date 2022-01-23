using UnityEngine;
using Zenject;

namespace GameShop
{
    public class GameInstance : MonoBehaviour
    {
        public CameraController CameraController { get; private set; }
        public ShopController ShopController { get; private set; }
        public StorageController StorageController { get; private set; }
        public WalletController WalletController { get; private set; }

        public static GameInstance Instance = null;

        [Inject]
        public void Costuctor(ShopController shopController, StorageController storageController
            ,WalletController walletController, CameraController cameraController)
        {
            ShopController = shopController;
            StorageController = storageController;
            WalletController = walletController;
            CameraController = cameraController;
        }

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            Destroy(gameObject);
        }
    }
}
