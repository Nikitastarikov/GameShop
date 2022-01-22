using UnityEngine;

namespace GameShop
{
    public interface IFactoryInterface
    {
        IProductModelInterface FactoryMethod(string name, int price);
    }
}

