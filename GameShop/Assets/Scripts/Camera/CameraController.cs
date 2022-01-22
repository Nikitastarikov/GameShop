using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameShop
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Button _nextItemButton;
        [SerializeField] private Button _previousItemButton;
        [SerializeField] private ShopController _shop;
        private Transform _tr;


        private void Start()
        {
            _shop = GameInstance.Instance.ShopController;
            _tr = GetComponent<Transform>();
            _nextItemButton.onClick.AddListener(FlipTheNextItem);
            _previousItemButton.onClick.AddListener(FlipThePreviousItem);
            //_nextItemButton.onClick.AddListener()
        }

        public void FlipTheNextItem()
        {
            if (_shop.Count < _shop.Goods.Count - 1)
            {
                _tr.position = new Vector3(_tr.position.x + _shop.DistanceBetweenGoods, _tr.position.y, _tr.position.z);
                _shop.IncrementCount();
            }
        }

        public void FlipThePreviousItem()
        {
            if (_shop.Count > 0)
            {
                _tr.position = new Vector3(_tr.position.x - _shop.DistanceBetweenGoods, _tr.position.y, _tr.position.z);
                _shop.DecrementCount();
            }
        }
    }
}
