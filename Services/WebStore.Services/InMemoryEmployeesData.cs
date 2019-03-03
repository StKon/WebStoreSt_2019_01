using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        //локальный источник данных
        private readonly List<EmployeeViewModel> _employee = new List<EmployeeViewModel>
        {
            new EmployeeViewModel {Id = 1, FirstName = "Иван", SecondName = "Иванов", Patronymic = "Иванович", Age = 22, BirthDate = new DateTime(1997, 7, 20), DateWork = new DateTime(2017, 6, 20)},
            new EmployeeViewModel {Id = 2, FirstName = "Владислав", SecondName = "Петров", Patronymic = "Иванович", Age = 35, BirthDate = new DateTime(1984, 1, 23), DateWork = new DateTime(2010, 5, 23)},
            new EmployeeViewModel {Id = 3, FirstName = "Станислав", SecondName = "Сидоров", Patronymic = "Петрович", Age = 53, BirthDate = new DateTime(1966, 1, 28), DateWork = new DateTime(2010, 10, 2)}
        };

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="newEmp"></param>
        public void AddNew(EmployeeViewModel newEmp)
        {
            //проверяем в списке
            if (newEmp is null)
                throw new ArgumentNullException(nameof(newEmp));

            //определяем id
            newEmp.Id = _employee.Count == 0 ? 1 : _employee.Max(e => e.Id) + 1;
            _employee.Add(newEmp);
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var emp = GetById(id);
            if (emp is null) return;

            _employee.Remove(emp);
        }

        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeViewModel> GetAll() => _employee;

        /// <summary>
        /// Получить сотрудника по индексу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeeViewModel GetById(int id) => _employee.FirstOrDefault(emp => emp.Id == id);

        /// <summary>
        /// Ничего не делает
        /// Сохранит изменения в БД
        /// </summary>
        public void SaveChanges()
        {  }

        /// <summary>
        /// Изменить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        public EmployeeViewModel UpdateEmployee(int id, EmployeeViewModel emp)
        {
            if (emp is null)
                throw new ArgumentNullException(nameof(emp));

            var employee = GetById(id);
            if (employee is null)
                throw new InvalidOperationException($"Сотрудник {emp.Id} не найден");

            employee.Age = emp.Age;
            employee.FirstName = emp.FirstName;
            employee.Patronymic = emp.Patronymic;
            employee.SecondName = emp.SecondName;
            employee.BirthDate = emp.BirthDate;
            employee.DateWork = emp.DateWork;
            return employee;
        }
    }
}
