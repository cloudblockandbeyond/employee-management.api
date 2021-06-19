using System;

namespace employee_management.api.Domain.DataTransferObjects
{
    public class EmployeeList
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }
    }
}
