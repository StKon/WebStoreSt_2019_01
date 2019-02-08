using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Infrastructure.Filters;
using System.Net;


namespace WebStore.Controllers
{
    //[Route("Users")]  //маршрут для всего контроллера один без [Route("Get")] не работает!!!
    //[TestActionFilter]  //AФильтр действия к контроллеру (все методы)
    //[ServiceFilter(typeof(TestResultFilter))]  //передаются параметры в фильтр
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
        //[Route("Get")]           
        //[TestActionFilter]  //AФильтр действия к методу
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
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest();  //параметра нет код ошибки 400
            EmployeeViewModel emp = _employee.FirstOrDefault(e => e.Id == id);
            if (emp is null) return NotFound();  //код ошибки 404
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
            if (id != null)
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
            if (!ModelState.IsValid) return View(emp);  //состояние модели

            //id == 0 - добавить запись
            if (emp.Id == 0)
            {
                emp.Id = _employee.Max(e => e.Id) + 1;
                _employee.Add(emp);
            }
            else
            {
                EmployeeViewModel oldEmp = _employee.FirstOrDefault(e => e.Id == emp.Id);
                if (oldEmp is null) return NotFound();
                _employee[_employee.IndexOf(oldEmp)] = emp;
                //oldEmp.FirstName = emp.FirstName;
                //oldEmp.SecondName = emp.SecondName;
                //oldEmp.Patronymic = emp.Patronymic;
                //oldEmp.DateWork = emp.DateWork;
                //oldEmp.Age = emp.Age;
                //oldEmp.BirthDate = emp.BirthDate;
            }
            return RedirectToAction("Index", "Employees");
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

        /// <summary>
        /// Возвращаемые результаты
        /// </summary>
        /// <returns></returns>
        public IActionResult TestAction()
        {
            //return new ContentResult();  //наиболее общий класс 
            //return new EmptyResult();  //пустой результат
            //return new FileResult();      //группа классов наследников. Передают файловую информацию
            //return new FileContentResult(); //передает массив байт
            //return new FileStreamResult();   //возвращает поток данных
            //return new StatusCodeResult(404);    //возвращает статусный код
            //return new UnauthorizedResult();    //пользователь не прошел автризацию. статусный код 401
            //return new JsonResult();             //возвращает результат в виде json объекта
            //return new PartialViewResult()   //частичное представление

            //return new RedirectResult();  //перенаправить пользователя на другой адрес
            //return Redirect();
            //return new RedirectToActionResult();  //выполняет переадресацию на определенный метод контроллера
            return null;
        }
    }
}