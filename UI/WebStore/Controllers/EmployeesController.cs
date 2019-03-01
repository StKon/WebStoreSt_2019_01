using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.ViewModels;
using WebStore.Infrastructure.Filters;
using WebStore.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Authorization;
using WebStore.Domain.Entities;

namespace WebStore.Controllers
{
    [Authorize]  //запретьть доступ к неавторизованным пользователям
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
        public IActionResult Index()
        {
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
        //[ValidateAntiForgeryToken]   //проверка запроса на достоверность
        [Authorize (Roles = WebStore.Domain.Entities.User.AdminRole)]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new EmployeeViewModel
                {
                    FirstName = "Имя",
                    SecondName = "Фамилия",
                    Patronymic = "Отчество"
                });

            EmployeeViewModel emp = _employeesData.GetById((int)id);
            if (emp is null) return NotFound();
            return View(emp);
        }

        /// <summary>
        /// Редактировать сотрудника
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = WebStore.Domain.Entities.User.AdminRole)]
        public IActionResult Edit(EmployeeViewModel emp)
        {
            if (!ModelState.IsValid)
            {
                //Генерируем ошибку формы
                //if (emp.Age % 2 == 0)
                //{
                //    ModelState.AddModelError("Ошибка", "Ошибка возраста");
                //}
                return View(emp);  //состояние модели
            }

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
        [Authorize(Roles = WebStore.Domain.Entities.User.AdminRole)]
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
        [Authorize(Roles = WebStore.Domain.Entities.User.AdminRole)]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();

            EmployeeViewModel emp = _employeesData.GetById((int)id);
            if (emp is null) return NotFound();

            _employeesData.Delete((int)id);
            return RedirectToAction("Index", "Employees");
        }
    }
}