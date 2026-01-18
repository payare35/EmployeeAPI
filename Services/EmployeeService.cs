using EmployeeAPI.Models;
using EmployeeAPI.Repositories;

namespace EmployeeAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _repo = employeeRepository;
        }

        public async Task AddEmployee(Employee employee)
        {
            await _repo.Add(employee);
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _repo.GetById(id);
            if(employee != null)
            {
                await _repo.Delete(employee);
            }
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _repo.GetAll();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await _repo.Update(employee);
        }
    }
}