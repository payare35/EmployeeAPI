using EmployeeAPI.Models;

namespace EmployeeAPI.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(int id);
        Task Add(Employee employee);
        Task Update(Employee employee);
        Task Delete(Employee employee);

    }
}