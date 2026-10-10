using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleRepository _repository;

        public ModulesController(IModuleRepository repository)
        {
            _repository = repository;
        }
       
        [HttpGet("module-show/{id}")]
        public async Task<ActionResult<Modules>> GetById(string id)
        {
            try
            {
                var module = await _repository.GetByIdAsync(id);
                if (module == null)
                {
                    return NotFound();
                }
                return Ok(new { data = module });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("module-index")]
        public async Task<ActionResult<IEnumerable<Modules>>> GetAll(int page = 0, int per_page = 10, string? search = null)
        {
            try
            {
                var modules = await _repository.GetAllAsync(page, per_page, search);
                var data = new
                {
                    data = modules
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("module-create")]
        public async Task<IActionResult> Create([FromBody] ModuleDto module)
        {
            if (module == null)
            {
                return BadRequest("module data is required.");
            }

            try
            {
                await _repository.CreateAsync(module);
                return Ok(new { data = "module Successfully Created" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("module-Update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ModuleDto module)
        {

            try
            {
                var existingModule = await _repository.GetByIdAsync(id);
                if (existingModule == null)
                {
                    return NotFound();
                }
                await _repository.UpdateAsync(id, module);
                return Ok(new { data = "Module Updated Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("module-Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}
