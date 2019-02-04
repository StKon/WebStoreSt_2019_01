using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        //локальный источник данных
        private static List<EmployeeViewModel> _employee = new List<EmployeeViewModel>
        {
            new EmployeeViewModel {Id = 1, FirstName = "Иван", SecondName = "Иванов", Patronymic = "Иванович", Age = 22, BirthDate = new DateTime(1997, 7, 20), DateWork = new DateTime(2017, 6, 20)},
            new EmployeeViewModel {Id = 2, FirstName = "Владислав", SecondName = "Петров", Patronymic = "Иванович", Age = 35, BirthDate = new DateTime(1984, 1, 23), DateWork = new DateTime(2010, 5, 23)},
            new EmployeeViewModel {Id = 3, FirstName = "Станислав", SecondName = "Сидоров", Patronymic = "Петрович", Age = 53, BirthDate = new DateTime(1966, 1, 28), DateWork = new DateTime(2010, 10, 2)}
        };

        /// <summary>
        /// Вывод списка сотрудников
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Просмотр карточки сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            //EmployeeViewModel emp = _employee.Where(e => e.Id == id).FirstOrDefault();
            EmployeeViewModel emp = _employee.FirstOrDefault(e => e.Id == id);
            if (emp is null) return NotFound();
            return View(emp);
        }

        /// <summary>
        /// Редактирование карточки сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id != null)
            {
                EmployeeViewModel emp = _employee.FirstOrDefault(e => e.Id == id);
                if (emp != null) return View(emp);
            }
            return NotFound();
        }

        /// <summary>
        /// Редактировать сотрудника
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel emp)
        {
            EmployeeViewModel oldEmp = _employee.FirstOrDefault(e => e.Id == emp.Id);
            if(oldEmp != null)
            {
                _employee[_employee.IndexOf(oldEmp)] = emp;
                return RedirectToAction("Index", "Employees");
            }
            return NotFound();
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult DeleteEmp(int? id)
        {
            if (id != null)
            {
                EmployeeViewModel emp = _employee.FirstOrDefault(e => e.Id == id);
                if (emp != null) return View(emp);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            EmployeeViewModel emp = _employee.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                _employee.Remove(emp);
                return RedirectToAction("Index", "Employees");
            }
            return NotFound();
        }
    }
}