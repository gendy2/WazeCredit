using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WazeCredit.Service.LifeTimeExample;

namespace WazeCredit.Controllers
{
    public class LifeTimeController : Controller
    {
        private readonly ScopedService _scopedService;
        private readonly TransientService _transientService;
        private readonly SingletonService _singletonService;

        public LifeTimeController(ScopedService scopedService,
            TransientService transientService,
            SingletonService singletonService)
        { 
            _transientService = transientService;
            _scopedService = scopedService;
            _singletonService = singletonService;
        }

        public IActionResult Index()
        {
            var message = new List<string>()
            {
                HttpContext.Items["CustomMiddlewareTransient"].ToString(),
                $"Transient Inside Controller {_transientService.GetGuid()}",
                HttpContext.Items["CustomMiddlewareScoped"].ToString(),
                $"Scoped Inside Controller {_scopedService.GetGuid()}",
                HttpContext.Items["CustomMiddlewareSingleton"].ToString(),
                $"Singleton Inside Controller {_singletonService.GetGuid()}"
            };
            return View("~/views/home/lifetime/index.cshtml", message);
        }
    }
}