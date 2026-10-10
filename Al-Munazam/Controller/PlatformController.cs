using Application.DTOs;
using Application.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepo;
        public PlatformController(IPlatformRepository platformRepo)
        {
            _platformRepo = platformRepo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _platformRepo.GetAllAsync());

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _platformRepo.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PlatformDto dto)
        {
                await _platformRepo.CreateAsync(dto);
                return Ok("Platform Created Successfully");
            
            
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PlatformDto dto)
        {
            await _platformRepo.UpdateAsync(id, dto);
            return Ok("Platform Updated Successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _platformRepo.DeleteAsync(id);
            return Ok("Platform Deleted Successfully");
        }
    }
}
