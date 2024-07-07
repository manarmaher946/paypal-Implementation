using PayPal.Api;
using PaypalDemo.Controllers;

namespace PaypalDemo.PaymentService
{
    public class ShipmentService :IPaymentService
    {
        private readonly PayPalSetting payPalSetting;

        public ShipmentService(PayPalSetting payPalSetting)
        {
            this.payPalSetting = payPalSetting;
        }
        public async Task<Payment> CreatePayment()
        {

            var dic = new Dictionary<string, string>();
            dic.Add("ClientId", payPalSetting.ClientId);
            dic.Add("ClientSecret", payPalSetting.ClientSecret);

            var moodDic = new Dictionary<string, string>();
            moodDic.Add("mood", payPalSetting.Mode);
            var _accessToken = new OAuthTokenCredential(payPalSetting.ClientId, payPalSetting.ClientSecret, moodDic).GetAccessToken();

            APIContext context = new APIContext(_accessToken);
            context.Config = moodDic;

            Payment createdPayment;
            try
            {
                Payment payment = new Payment()
                {
                    intent = "sale",
                    payer = new Payer { payment_method = "paypal" },
                    transactions =
                    new List<Transaction>()
                    {
                        new Transaction()
                        {
                            amount = new Amount
                            {
                                currency ="USA",
                                total = "100"
                            },
                            description ="TEST"
                        },

                    },
                    redirect_urls = new RedirectUrls
                    {
                        cancel_url = "http://localhost:55958/paypal/cancel",
                        return_url = "http://localhost:55958/paypal/success"
                    }
                };
                createdPayment = await Task.Run(() => payment.Create(context));


            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }

            return createdPayment;
        }
    }
}
