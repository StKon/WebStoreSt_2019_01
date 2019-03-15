using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebStore.Services.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;  //операция
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ErrorHandlingMiddleware));

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary> обработка операции  </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception err)
            {
                await HandleExceptionAsync(context, err);
                throw;
            }
        }

        /// <summary> обработка исключения </summary>
        private static Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            _log.Error(error.Message, error);  //лог исключения
            return Task.CompletedTask;
        }
    }
}
