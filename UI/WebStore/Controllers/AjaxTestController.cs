using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class AjaxTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Запрос выдает частичное представление с датой и временем
        public async Task<IActionResult> GetTestView()
        {
            //задержка
            await Task.Delay(2000);

            //частичное представление
            return PartialView("Partial/_DataView", DateTime.Now);
        }

        //запрос возвращает данные в json
        public async Task<IActionResult> GetJson(int? id, string msg)
        {
            //задержка
            await Task.Delay(2000);

            return Json(new { Message = $"Возвращаем {id ?? -1} : {msg ?? "null"}", ServerTime = DateTime.Now });
        }

        /// <summary> Тест signalr </summary>
        public IActionResult SignalRTest() => View();
    }
}