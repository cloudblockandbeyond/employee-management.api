using System;

namespace employee_management.api.Domain.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeEmail { get; set; }

        public string EmployeeImage { get; set; }

        public int DepartmentId { get; set; }
    }
}
