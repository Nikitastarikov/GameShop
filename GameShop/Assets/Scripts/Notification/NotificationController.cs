using UnityEngine;
using UnityEngine.UI;

namespace GameShop
{
    public class NotificationController
    {
        private GameObject _notificationBar;
        private Text _textField;

        public void SetNotificationBar(GameObject notificationBar)
        {
            _notificationBar = notificationBar;
            foreach (Transform tr in _notificationBar.transform)
            {
                if (tr.TryGetComponent(out Text text))
                {
                    _textField = text;
                    break;
                }
            }
        }

        public void ShowNotification(string notification)
        {
            _notificationBar.SetActive(true);
            _textField.text = notification;
        }
    }
}
