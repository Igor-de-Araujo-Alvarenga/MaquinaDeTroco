namespace Domain
{
    public class Money : Entity
    {
        public double MoneyValue { get; set; }
        public int Quantity { get; set; }

        public Money(double moneyValue, int quantity)
        {
            MoneyValue = moneyValue;
            Quantity = quantity;
        }
    }
}