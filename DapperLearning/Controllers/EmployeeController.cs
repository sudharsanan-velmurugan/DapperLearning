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

        // Repository pattern using Dependency Injection

        public EmployeeController(IEmployeeRepository EmployeeRepository)
        {
            employeeRepository = EmployeeRepository;
        }


        // Get all employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await employeeRepository.GetAllAsync();
            return Ok(employees);
        }

        // Getting single employee by his id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if(employee == null) 
                return NotFound();
            return Ok(employee);
        }

        // Adding new employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {

            await employeeRepository.AddEmployeeAsync(employee);
            return Ok();
        }

        //Editing employee details
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

        // Removing employee 
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
