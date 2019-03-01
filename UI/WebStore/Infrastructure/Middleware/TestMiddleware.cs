using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace WebStore.Infrastructure.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _nextAction;
        //private readonly IServiceCollection _services;

        //public TestMiddleware(RequestDelegate nextAction, IServiceCollection services, IConfiguration configuration)  //не работает
        //public TestMiddleware(RequestDelegate nextAction, IServiceCollection services) //не работает
        public TestMiddleware(RequestDelegate nextAction)
        {
            _nextAction = nextAction;
        }

        public async Task Invoke(HttpContext ctx)
        {

            await _nextAction(ctx);

        }
    }
}
