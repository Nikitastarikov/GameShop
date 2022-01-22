namespace GameShop
{
    public class FactoryCube : IFactoryInterface
    {
        public IProductModelInterface FactoryMethod(string name, int price)
        {
            return new ProductModelCube(name, price);
        }
    }
}