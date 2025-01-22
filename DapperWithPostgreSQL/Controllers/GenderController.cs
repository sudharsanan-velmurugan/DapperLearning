using DapperWithPostgreSQL.Models;
using DapperWithPostgreSQL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWithPostgreSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly Repository.IGenericRepository<Gender> repo;

        public GenderController(IGenericRepository<Gender> repo)
        {
            this.repo = repo;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllAsync()
        {
            var genders = await repo.GetAllAsync();
            if (!genders.Any())
                return NoContent();
            return Ok(genders);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetGenderIdAsync(int id)
        {
            var gender = await repo.GetByIdAsync(id);
            if (gender == null)
                return NotFound();
            return Ok(gender);
        }
        [HttpPost]

        public async Task<IActionResult> AddGenderAsync(Gender gender)
        {
            if (gender == null)
            return BadRequest();

            await repo.AddAsync(gender);
            return Created("api/gender", gender);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateGenderAsync(int id,Gender gender)
        {
            var genderExists = await repo.GetByIdAsync(id);
            if (genderExists == null)
                return NotFound();
            gender.Id = genderExists.Id;
            await repo.UpdateAsync(gender);
            return Ok(gender);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletebyIdAsync(int id)
        {
            var genderExists = await repo.GetByIdAsync(id);
            if (genderExists == null)
                return NotFound();
           
            await repo.DeleteAsync(id);
            return Ok("Deleted Successfully");
        }
    }
}
