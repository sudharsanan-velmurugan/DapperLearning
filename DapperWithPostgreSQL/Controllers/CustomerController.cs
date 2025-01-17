using DapperWithPostgreSQL.Models;
using DapperWithPostgreSQL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWithPostgreSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericRepository<Customer> _repo;

        public CustomerController(IGenericRepository<Customer> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var customers = await _repo.GetAllAsync();
            return Ok(customers);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomerById(int Id)
        {
            var customers = await _repo.GetByIdAsync(Id);
            if (customers == null) 
                return NotFound();

            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            if(customer == null)
                return BadRequest();
            await _repo.AddAsync(customer);
            return CreatedAtAction(nameof(AddCustomer), new {Id=customer.Id}, customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id,Customer customer)
        {
            var customerExists = await _repo.GetByIdAsync(id);
            if (customerExists == null)
                return NotFound();
            customer.Id = customerExists.Id;

            await _repo.UpdateAsync(customer);

            return Ok(customer);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCustomerById(int Id)
        {
            var customerExists = await _repo.GetByIdAsync(Id);
            if (customerExists == null)
                return NotFound();

            await _repo.DeleteAsync(Id);

            return Ok();
        }

        [HttpGet("GetCustomerWithGender")]
        public async Task<IActionResult> GetWithGender()
        {
            var customers = await _repo.GetCustomersWithGenderAsync();
            return Ok(customers);
        }
    }
}
