namespace BBTest
{
    public interface ICheckout
    {
        decimal CalculateTotal(IBasket basket);
    }
}