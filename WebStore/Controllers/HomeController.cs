using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        //локальный источник данных
        private static List<EmployeeViewModel> _employee = new List<EmployeeViewModel>
        {
            new EmployeeViewModel {Id = 1, FirstName = "Иван", SecondName = "Иванов", Patronymic = "Иванович", Age = 22},
            new EmployeeViewModel {Id = 2, FirstName = "Владислав", SecondName = "Петров", Patronymic = "Иванович", Age = 35}
            new EmployeeViewModel {Id = 3, FirstName = "Станислав", SecondName = "Сидоров", Patronymic = "Петрович", Age = 53}
        };

        public IActionResult Index()
        {
            
            //return Content("Hello from controller");
            //return Accepted();
            //return BadRequest();
            //return NotFound();
            //return Json();
            //return PartialView();
            return View();
        }
    }
}