using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Api;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        //Можно так
        //private readonly IValuesService _valuesService;
        //
        //public HomeController(IValuesService valuesService)
        //{
        //    _valuesService = valuesService;
        //}

        public IActionResult Index() => View();

        public IActionResult ContactUs() => View();

        public IActionResult Cart() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Blog() => View();

        public IActionResult ErrorPage404() => View();

        public IActionResult ValuesServiceTest([FromServices] IValuesService valuesService) => View(valuesService.Get());

        public IActionResult ErrorStatus(string id)
        {
            if (id == "404")
                return RedirectToAction("ErrorPage404");
            return Content($"Статуcный код ошибки: {id} ");
        }
    }
}