using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using employee_management.api.Domain.Models;
using employee_management.api.Domain.DataTransferObjects;


namespace employee_management.api.DataAccess.Services
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeList>> GetEmployeeList();

        Task<EmployeeDetails> GetEmployeeDetails(int id);

        Task<EmployeeDetails> CreateEmployee(Employee employee);

        Task<EmployeeDetails> UpdateEmployee(Employee employee);

        Task<EmployeeList> DeleteEmployee(Employee employee);
    }
}
