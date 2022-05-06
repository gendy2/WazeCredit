using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WazeCredit.Service.LifeTimeExample;

namespace WazeCredit.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext,
            TransientService transientService,
            ScopedService scopedService,
            SingletonService singletonService
            )
        {
            httpContext.Items.Add("CustomMiddlewareTransient", $"Transient Middleware - {transientService.GetGuid()}");
            httpContext.Items.Add("CustomMiddlewareScoped", $"Scoped Middleware - {scopedService.GetGuid()}");
            httpContext.Items.Add("CustomMiddlewareSingleton", $"Singleton Middleware - {singletonService.GetGuid()}");

            await _next(httpContext);
        }


    }
}