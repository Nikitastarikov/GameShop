using UnityEngine;
using UnityEngine.UI;

namespace GameShop
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] private Text _money;
        [SerializeField] private Text _name;

        public Text GetMoney() => _money;
        public Text GetName() => _name;

        public void SetMoney(int money) => _money.text = money.ToString();
        public void SetName(string name) => _name.text = name;
    }
}
