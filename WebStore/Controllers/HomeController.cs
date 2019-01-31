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
            new EmployeeViewModel {Id = 1, FirstName = "Иван", SecondName = "Иванов", Patronymic = "Иванович", Age = 22, BirthDate = new DateTime(1997, 7, 20), DateWork = new DateTime(2017, 6, 20)},
            new EmployeeViewModel {Id = 2, FirstName = "Владислав", SecondName = "Петров", Patronymic = "Иванович", Age = 35, BirthDate = new DateTime(1984, 1, 23), DateWork = new DateTime(2010, 5, 23)},
            new EmployeeViewModel {Id = 3, FirstName = "Станислав", SecondName = "Сидоров", Patronymic = "Петрович", Age = 53, BirthDate = new DateTime(1966, 1, 28), DateWork = new DateTime(2010, 10, 2)}
        };

        public IActionResult Index()
        {
            
            //return Content("Hello from controller");
            //return Accepted();
            //return BadRequest();
            //return NotFound();
            //return Json();
            //return PartialView();
            //return View();
            return View(_employee);  //передаем модель
        }

        public IActionResult Details(int id)
        {
            EmployeeViewModel emp = _employee.Where(e => e.Id == id).FirstOrDefault();

            return View(emp);
        }
    }
}