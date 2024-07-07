
using Microsoft.Extensions.Configuration;
using PaypalDemo.PaymentService;

namespace PaypalDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IPaymentService, ShipmentService>();

            var paypalSetting = builder.Configuration.GetSection("PayPalSetting").Get<PayPalSetting>();
            if(paypalSetting != null )
            {
                builder.Services.AddSingleton(paypalSetting);
            }
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
