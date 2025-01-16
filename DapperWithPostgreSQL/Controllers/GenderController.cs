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

        public async Task<IActionResult> Get()
        {
            var genders = await repo.GetAllAsync();
            return Ok(genders);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var gender = await repo.GetByIdAsync(id);
            if (gender == null)
                return NotFound();
            return Ok(gender);
        }
        [HttpPost]

        public async Task<IActionResult> Post(Gender gender)
        {
            if (gender == null)
            return BadRequest();

            await repo.AddAsync(gender);
            return CreatedAtAction(nameof(Post),new {id=gender.Id},gender);
        }
    }
}
