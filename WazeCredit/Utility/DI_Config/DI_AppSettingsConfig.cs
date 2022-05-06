using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WazeCredit.Utility.AppSettingsClasses;

namespace WazeCredit.Utility.DI_Config
{
    public static class DI_AppSettingsConfig
    {
        

        public static IServiceCollection AddAppSerttingsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SendGridSettings>(configuration.GetSection("SendGrid"));
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
            services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
            services.Configure<WazeForecastSettings>(configuration.GetSection("WazeForecast"));
            return services;
        }
        
    }
}