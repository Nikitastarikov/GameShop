using UnityEngine;
using Zenject;

namespace GameShop
{
    public class GameInstance : MonoBehaviour
    {
        public NotificationController NotificationController { get; private set; }
        public CameraController CameraController { get; private set; }
        public ShopController ShopController { get; private set; }
        public StorageControllerAntyhack StorageController { get; private set; }
        public WalletController WalletController { get; private set; }

        public static GameInstance Instance = null;
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

        [Inject]
        public void Costuctor(ShopController shopController, StorageControllerAntyhack storageController
            ,WalletController walletController, CameraController cameraController
            ,NotificationController notificationController)
        {
            ShopController = shopController;
            StorageController = storageController;
            WalletController = walletController;
            CameraController = cameraController;
            NotificationController = notificationController;
        }

        [ContextMenu("ResetGame")]
        public void ResetGame()
        {
            StorageControllerAntyhack.DeleteAll();
        }
    }
}
