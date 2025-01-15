using DapperLearning.Models;
using DapperLearning.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository EmployeeRepository)
        {
            employeeRepository = EmployeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await employeeRepository.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if(employee == null) 
                return NotFound();
            return Ok(employee);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {

            await employeeRepository.AddEmployeeAsync(employee);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id,Employee employee)
        {
            var existEmployee = await employeeRepository.GetByIdAsync(id);

            if (existEmployee == null)
                return NotFound();
            employee.Id = id;
            await employeeRepository.UpdateEmployeeAsync(id, employee);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var existEmployee = await employeeRepository.GetByIdAsync(id);

            if (existEmployee == null)
                return NotFound();
            
            await employeeRepository.DeleteEmployeeAsync(id);
            return Ok();
        }
    }
}
