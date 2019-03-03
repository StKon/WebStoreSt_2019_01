using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels;

namespace WebStore.Interfaces
{
    /// <summary>
    /// Работа с сотрудниками
    /// </summary>
    public interface IEmployeesData
    {
        /// <summary>
        /// Возвращает всех сотрудников
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmployeeViewModel> GetAll();

        /// <summary>
        /// Возвращает сотрудника по идентификатору
        /// </summary>
        /// <returns></returns>
        EmployeeViewModel GetById(int id);

        /// <summary>
        /// Изменить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        EmployeeViewModel UpdateEmployee(int id, EmployeeViewModel emp);

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="emp"></param>
        void AddNew(EmployeeViewModel newEmp);

        /// <summary>
        /// Удалять сотрудника
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        void SaveChanges();
    }
}
