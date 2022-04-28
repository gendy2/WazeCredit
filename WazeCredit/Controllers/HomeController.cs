using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WazeCredit.Models;
using WazeCredit.Models.ViewModels;
using WazeCredit.Service;

namespace WazeCredit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMarketForecaster _marketForecaster;
        // private readonly MarketForecasterV2 _marketForecasterV2;
        //
        // public HomeController( MarketForecasterV2 marketForecasterV2)
        // {
        //     _marketForecasterV2 = marketForecasterV2;
        // }
        // private readonly ILogger<HomeController> _logger;
        //
        // public HomeController(ILogger<HomeController> logger)
        // {
        //     _logger = logger;
        // }

        public HomeViewModel ObjHomeViewModel { get; set; }
        public HomeController(IMarketForecaster marketForecaster) 
        {
            _marketForecaster = marketForecaster;
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
