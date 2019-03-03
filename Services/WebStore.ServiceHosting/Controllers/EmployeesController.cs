using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.ViewModels;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmployeesController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="employeesData"></param>
        public EmployeesController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        [HttpPost, ActionName("Post")]
        public void AddNew([FromBody] EmployeeViewModel newEmp)
        {
            _employeesData.AddNew(newEmp);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeesData.Delete(id);
        }

        [HttpGet, ActionName("Get")]
        public IEnumerable<EmployeeViewModel> GetAll()
        {
            return _employeesData.GetAll();
        }

        [HttpGet("{id}"), ActionName("Get")]
        public EmployeeViewModel GetById(int id)
        {
            return GetById(id);
        }

        [NonAction]
        public void SaveChanges()
        {
            _employeesData.SaveChanges();
        }

        [HttpPut("{id}"), ActionName("Put")]
        public EmployeeViewModel UpdateEmployee(int id, [FromBody] EmployeeViewModel emp)
        {
            return UpdateEmployee(id, emp);
        }
    }
}