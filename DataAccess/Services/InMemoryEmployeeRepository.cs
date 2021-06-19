using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using employee_management.api.Domain.Models;
using employee_management.api.Domain.DataTransferObjects;

namespace employee_management.api.DataAccess.Services
{
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Department> _departments;
        private readonly List<Employee> _employees;

        public InMemoryEmployeeRepository()
        {
            this._departments = new List<Department>()
            {
                new Department() { DepartmentId = 1, DepartmentName = "None" },
                new Department() { DepartmentId = 2, DepartmentName = "IT" },
                new Department() { DepartmentId = 3, DepartmentName = "HR" },
                new Department() { DepartmentId = 4, DepartmentName = "Payroll" }
            };

            this._employees = new List<Employee>()
            {
                new Employee() {
                    EmployeeId = 1, EmployeeName = "John", EmployeeEmail = "john@employeemanagement.com"
                    , EmployeeImage = "http://localhost:5000/images/john.png", DepartmentId = this._departments[1].DepartmentId
                },
                new Employee() {
                    EmployeeId = 2, EmployeeName = "Mary", EmployeeEmail = "mary@employeemanagement.com"
                    , EmployeeImage = "http://localhost:5000/images/mary.png", DepartmentId = this._departments[2].DepartmentId
                },
                new Employee() {
                    EmployeeId = 3, EmployeeName = "Sara", EmployeeEmail = "sara@employeemanagement.com"
                    , EmployeeImage = "http://localhost:5000/images/sara.png", DepartmentId = this._departments[3].DepartmentId
                },
            };
        }

        public Task<IEnumerable<EmployeeList>> GetEmployeeList()
        {
            return Task.Run(() => {
                IEnumerable<EmployeeList> employeeLists = this._employees.Select((employee) => {
                    return new EmployeeList()
                    {
                        Id = employee.EmployeeId,
                        Email = employee.EmployeeEmail,
                        Image = employee.EmployeeImage
                    };
                });

                return employeeLists;
            });
        }

        public Task<EmployeeDetails> GetEmployeeDetails(int id)
        {
            return Task.Run(() => {
                EmployeeDetails employeeDetails = (
                    from employee in this._employees
                    join department in this._departments on employee.DepartmentId equals department.DepartmentId
                    where employee.EmployeeId == id
                    select new EmployeeDetails()
                    {
                        Id = employee.EmployeeId,
                        Name = employee.EmployeeName,
                        Department = department.DepartmentName,
                        Email = employee.EmployeeEmail,
                        Image = employee.EmployeeImage
                    }
                ).FirstOrDefault();

                return employeeDetails;
            });
        }

        public Task<EmployeeDetails> CreateEmployee(Employee createEmployee)
        {
            return Task.Run(() => {
                int id = this._employees.Max((employee) => (employee.EmployeeId)) + 1;
                createEmployee.EmployeeId = id;
                this._employees.Add(createEmployee);

                EmployeeDetails employeeDetails = (
                    from employee in this._employees
                    join department in this._departments on employee.DepartmentId equals department.DepartmentId
                    where employee.EmployeeId == createEmployee.EmployeeId
                    select new EmployeeDetails()
                    {
                        Id = employee.EmployeeId,
                        Name = employee.EmployeeName,
                        Department = department.DepartmentName,
                        Email = employee.EmployeeEmail,
                        Image = employee.EmployeeImage
                    }
                ).FirstOrDefault();

                return employeeDetails;
            });
        }

        public Task<EmployeeDetails> UpdateEmployee(Employee updateEmployee)
        {
            return Task.Run(() => {
                EmployeeDetails employeeDetails = null;

                Employee oldEmployee = this._employees.FirstOrDefault((employee) => (employee.EmployeeId == updateEmployee.EmployeeId));

                if (oldEmployee is not null)
                {
                    this._employees.Remove(oldEmployee);
                    this._employees.Add(updateEmployee);

                    employeeDetails = (
                        from employee in this._employees
                        join department in this._departments on employee.DepartmentId equals department.DepartmentId
                        where employee.EmployeeId == updateEmployee.EmployeeId
                        select new EmployeeDetails()
                        {
                            Id = employee.EmployeeId,
                            Name = employee.EmployeeName,
                            Department = department.DepartmentName,
                            Email = employee.EmployeeEmail,
                            Image = employee.EmployeeImage
                        }
                    ).FirstOrDefault();
                }

                return employeeDetails;
            });
        }

        public Task<EmployeeList> DeleteEmployee(Employee deleteEmployee)
        {
            return Task.Run(() => {
                EmployeeList employeeList = null;

                Employee removeEmployee = this._employees.FirstOrDefault((e) => (e.EmployeeId == deleteEmployee.EmployeeId));

                if (removeEmployee is not null)
                {
                    this._employees.Remove(removeEmployee);

                    employeeList = new EmployeeList()
                    {
                        Id = removeEmployee.EmployeeId,
                        Email = removeEmployee.EmployeeEmail,
                        Image = removeEmployee.EmployeeImage
                    };
                }

                return employeeList;
            });
        }
    }
}
