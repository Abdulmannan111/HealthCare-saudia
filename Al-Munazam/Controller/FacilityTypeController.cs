using Microsoft.AspNetCore.Mvc;
using Application.IRepositories;
using Application.Entities.Facility;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityTypeController : ControllerBase
    {
        private readonly IFacilityTypeRepository _repository;

        public FacilityTypeController(IFacilityTypeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<FacilityType>>> Get()
        {
            var facilityTypes = await _repository.GetAllAsync();
            return Ok(facilityTypes);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<FacilityType>> Get(string id)
        {
            var facilityType = await _repository.GetByIdAsync(id);
            if (facilityType == null)
            {
                return NotFound();
            }
            return Ok(facilityType);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] FacilityType facilityType)
        {
            await _repository.CreateAsync(facilityType);
            return CreatedAtAction(nameof(Get), new { id = facilityType.ID.ToString() }, facilityType);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Put(string id, [FromBody] FacilityType facilityType)
        {
            var existingFacilityType = await _repository.GetByIdAsync(id);
            if (existingFacilityType == null)
            {
                return NotFound();
            }
            var _FacilityType = new FacilityType 
            { 
                ID = existingFacilityType.ID,
                Name = facilityType.Name,
                Description = facilityType.Description,
            };
            await _repository.UpdateAsync(id, _FacilityType);
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingFacilityType = await _repository.GetByIdAsync(id);
            if (existingFacilityType == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}

