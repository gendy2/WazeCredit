using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WazeCredit.Models;
using WazeCredit.Models.ViewModels;
using WazeCredit.Service;
using WazeCredit.Utility.AppSettingsClasses;

namespace WazeCredit.Controllers
{
    public class HomeController : Controller
    {
        #region DI Fields

        private readonly IMarketForecaster _marketForecaster;
        private readonly SendGridSettings _sendGridOptions;
        private readonly StripeSettings _stripeOptions;
        private readonly TwilioSettings _twilioOptions;
        private readonly WazeForecastSettings _wazeForecastOptions;

        #endregion
        

        public HomeViewModel ObjHomeViewModel { get; set; }
        public HomeController(IMarketForecaster marketForecaster,
            IOptions<SendGridSettings> sendGridOptions,
            IOptions<StripeSettings> stripeOptions,
            IOptions<TwilioSettings> twilioOptions,
            IOptions<WazeForecastSettings> wazeForecastOptions) 
        {
            _marketForecaster = marketForecaster;
            _sendGridOptions = sendGridOptions.Value;
            _stripeOptions = stripeOptions.Value;
            _twilioOptions = twilioOptions.Value;
            _wazeForecastOptions = wazeForecastOptions.Value;
            ObjHomeViewModel = new HomeViewModel();
        }

        public IActionResult Index()
        {
            
            MarketResult currentMarket = _marketForecaster.GetMarketPrediction();

            switch (currentMarket.MarketCondition)
            {
                case MarketCondition.StableUp:
                    ObjHomeViewModel.MarketForecast = "Market Condition is : Stable Up";
                    break;
                case MarketCondition.StableDown:
                    ObjHomeViewModel.MarketForecast = "Market Condition is : Stable Down";
                    break;
                case MarketCondition.Volatile:
                    ObjHomeViewModel.MarketForecast = "Market Condition is : Volatile";
                    break;
                default:
                    ObjHomeViewModel.MarketForecast = "Market Condition is : Can't predict market behavior";
                    break;
            }
            return View(ObjHomeViewModel);
        }

        public IActionResult AllConfigSettings()
        {
            return View( new List<string>()
            {
                $"Waze Config - Forecast Tracker : {_wazeForecastOptions.ForecastTrackerEnabled}",
                $"SendGrid Config - SendGrid Key : {_sendGridOptions.SendGridKey}",
                $"Stripe Config - PublishableKey : {_stripeOptions.PublishableKey}, " +
                $"SecretKey : {_stripeOptions.SecretKey}",
                $"Twilio Config - Account Sid : {_twilioOptions.AccountSid}, " +
                $"Auth Token : {_twilioOptions.AuthToken}, " +
                $"Phone Number : {_twilioOptions.PhoneNumber}"
                
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
