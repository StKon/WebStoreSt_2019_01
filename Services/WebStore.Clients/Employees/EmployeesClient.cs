using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration configuration) :base( configuration)
        {
            ServicesAddress = "api/employees";
        }

        public IEnumerable<EmployeeViewModel> GetAll()
        {
            var url = $"{ServicesAddress}";
            var list = Get<List<EmployeeViewModel>>(url);
            return list;
        }

        public EmployeeViewModel GetById(int id)
        {
            var url = $"{ServicesAddress}/{id}";
            var result = Get<EmployeeViewModel>(url);
            return result;
        }

        public EmployeeViewModel UpdateEmployee(int id, EmployeeViewModel model)
        {
            var url = $"{ServicesAddress}/{id}";
            var response = Put(url, model);
            var result = response.Content.ReadAsAsync<EmployeeViewModel>().Result;
            return result;
        }

        public void AddNew(EmployeeViewModel model)
        {
            var url = $"{ServicesAddress}";
            Post(url, model);
        }
        public void Delete(int id)
        {
            var url = $"{ServicesAddress}/{id}";
            Delete(url);
        }

        public void SaveChanges()
        {  }
    }
}
