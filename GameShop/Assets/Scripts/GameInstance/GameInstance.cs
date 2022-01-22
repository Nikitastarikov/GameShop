using UnityEngine;
using Zenject;

namespace GameShop
{
    public class GameInstance : MonoBehaviour
    {
        public ShopController ShopController { get; private set; }
        public StorageController StorageController { get; private set; }

        public static GameInstance Instance = null;

        [Inject]
        public void Costuctor(ShopController shopController, StorageController storageController)
        {
            ShopController = shopController;
            StorageController = storageController;
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
