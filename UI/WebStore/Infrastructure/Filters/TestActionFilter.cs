using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebStore.Infrastructure.Filters
{
    /// <summary>
    /// Фильтр действий
    /// </summary>
    public class TestActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }

    /// <summary>
    /// Фильтр результата
    /// </summary>
    public class TestResultFilter : Attribute, IResultFilter
    {
        private readonly ILogger _logger;

        public TestResultFilter(ILogger logger) => _logger = logger;

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation($"Controller {context.Controller} invoked.");
        }
    }
}
