using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using employee_management.api.DataAccess.Services;
using Microsoft.AspNetCore.Http;
using employee_management.api.Domain.Models;
using employee_management.api.Domain.DataTransferObjects;


namespace employee_management.api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                IEnumerable<EmployeeList> employeeLists = await this._employeeRepository.GetEmployeeList();

                if (employeeLists is null)
                    return StatusCode(StatusCodes.Status204NoContent);
                else
                    return StatusCode(StatusCodes.Status200OK, employeeLists);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                EmployeeDetails employeeDetails = await this._employeeRepository.GetEmployeeDetails(id);

                if (employeeDetails is null)
                    return StatusCode(StatusCodes.Status400BadRequest);
                else
                    return StatusCode(StatusCodes.Status200OK, employeeDetails);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            try
            {
                EmployeeDetails employeeDetails = await this._employeeRepository.CreateEmployee(employee);

                if (employeeDetails is null)
                    return StatusCode(StatusCodes.Status400BadRequest);
                else
                    return StatusCode(StatusCodes.Status200OK, employeeDetails);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Employee employee)
        {
            try
            {
                EmployeeDetails employeeDetails = await this._employeeRepository.UpdateEmployee(employee);

                if (employeeDetails is null)
                    return StatusCode(StatusCodes.Status400BadRequest);
                else
                    return StatusCode(StatusCodes.Status200OK, employeeDetails);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Employee employee)
        {
            try
            {
                EmployeeList employeeList = await this._employeeRepository.DeleteEmployee(employee);

                if (employeeList is null)
                    return StatusCode(StatusCodes.Status400BadRequest);
                else
                    return StatusCode(StatusCodes.Status200OK, employeeList);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
    }
}
