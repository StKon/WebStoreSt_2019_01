using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Infrastructure.Filters;
using WebStore.Infrastructure.Interfaces;
using System.Net;


namespace WebStore.Controllers
{
    //[Route("Users")]  //маршрут для всего контроллера один без [Route("Get")] не работает!!!
    //[TestActionFilter]  //AФильтр действия к контроллеру (все методы)
    //[ServiceFilter(typeof(TestResultFilter))]  //передаются параметры в фильтр
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData empData)
        {
            _employeesData = empData;
        }

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
            return View(_employeesData.Get());  //передаем модель
        }

        /// <summary>
        /// Просмотр карточки сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest();  //параметра нет код ошибки 400
            EmployeeViewModel emp = _employeesData.GetById((int)id);
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
                EmployeeViewModel emp = _employeesData.GetById((int)id);
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
                _employeesData.AddNew(emp);
            }
            else
            {
                EmployeeViewModel oldEmp = _employeesData.GetById((int)emp.Id);
                if (oldEmp is null) return NotFound();

                oldEmp.FirstName = emp.FirstName;
                oldEmp.SecondName = emp.SecondName;
                oldEmp.Patronymic = emp.Patronymic;
                oldEmp.DateWork = emp.DateWork;
                oldEmp.Age = emp.Age;
                oldEmp.BirthDate = emp.BirthDate;
            }
            return RedirectToAction("Index", "Employees");
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult DeleteEmp(int? id)
        {
            if (id != null)
            {
                EmployeeViewModel emp = _employeesData.GetById((int)id);
                if (emp != null) return View(emp);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();

            EmployeeViewModel emp = _employeesData.GetById((int)id);
            if (emp is null) return NotFound();
            
            _employeesData.Delete((int)id);
            return RedirectToAction("Index", "Employees");
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