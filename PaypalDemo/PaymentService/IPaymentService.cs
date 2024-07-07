using PayPal.Api;

namespace PaypalDemo.PaymentService
{
    public interface IPaymentService
    {
       Task<Payment> CreatePayment();
    }
}
