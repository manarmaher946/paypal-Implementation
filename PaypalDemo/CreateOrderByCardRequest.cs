namespace PaypalDemo
{
    public class CreateOrderByCardRequest
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Name { get; set; }
    }
}
