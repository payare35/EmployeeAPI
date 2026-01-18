

using EmployeeAPI.Models;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace EmployeeAPI.Controllers
{   
    [Authorize]
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService employeeService)
        {
            _service = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetEmployeeById(id);
            if(employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _service.AddEmployee(employee);
            return Ok("Employee added successfully");
        } 

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            employee.Id = id;
            await _service.UpdateEmployee(employee);
            return Ok("Employee data updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteEmployee(id);
            return Ok("Employee data deleted successfully");
        }

    }
}