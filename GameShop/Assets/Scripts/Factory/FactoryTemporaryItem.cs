namespace GameShop
{
    public class FactoryTemporaryItem : IFactoryInterface
    {
        public IProductModelInterface FactoryMethod(string name, int price, int time)
        {
            return new ProductModelTemporaryItem(name, price, time);
        }
    }
}
