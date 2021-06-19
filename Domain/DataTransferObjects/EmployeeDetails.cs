using System;

namespace employee_management.api.Domain.DataTransferObjects
{
    public class EmployeeDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }

        public string Department { get; set; }
    }
}
