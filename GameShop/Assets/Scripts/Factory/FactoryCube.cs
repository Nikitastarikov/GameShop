namespace GameShop
{
    public class FactoryCube : IFactoryInterface
    {
        public IProductModelInterface FactoryMethod(string name, int price, int time)
        {
            return new ProductModelCube(name, price);
        }
    }
}