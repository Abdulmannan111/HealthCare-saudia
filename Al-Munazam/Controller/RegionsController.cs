using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionService;

        public RegionsController(IRegionRepository regionService)
        {
            _regionService = regionService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Regions>>> GetAll(int page = 1, int per_page = 10, string submission_for = null)
        {
            try
            {
                var regions = await _regionService.GetAllAsync(page, per_page, submission_for);
                var data = new
                {
                    data = regions
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("export")]
        public async Task<ActionResult<IEnumerable<Regions>>> export(int page = 1, int per_page = 10, string submission_for = null)
        {
            var regions = await _regionService.GetAllAsync(page, per_page, submission_for);
            var data = new
            {
                data = regions
            };
            return Ok(data);
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<Regions> GetById(string id)
        {
            try
            {
                var region =  _regionService.GetByIdAsync(id);
                if (region == null)
                {
                    return NotFound();
                }
                return Ok(new { data = region });

            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateRegion")]
        public async Task<IActionResult> Create([FromBody] RegionDto region)
        {
            if (region == null)
            {
                return BadRequest("Region data is required.");
            }

            try
            {
                await _regionService.CreateAsync(region);
                return Ok(new { data = "Region Successfully Created" });
            }
            catch (Exception ex)
            {
                if (ex.Message == "The name has already been taken.")
                {
                    return BadRequest(new
                    {
                        message = "The name has already been taken.",
                        errors = new { name = new[] { "The name has already been taken." } }
                    });
                }

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] RegionDto region)
        {
            try
            {
                var existingRegion =  _regionService.GetByIdAsync(id);
                if (existingRegion == null)
                {
                    return NotFound();
                }
                await _regionService.UpdateAsync(id, region);
                return Ok(new { data = "Region Updated Successfully" });
            }
            catch (Exception ex)
            {
                if (ex.Message == "The name has already been taken.")
                {
                    return BadRequest(new
                    {
                        message = "The name has already been taken.",
                        errors = new { name = new[] { "The name has already been taken." } }
                    });
                }

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var region = _regionService.GetByIdAsync(id);
                if (region == null)
                {
                    return NotFound();
                }

                await _regionService.DeleteAsync(id);
                return Ok(new { data = "Region Successfully Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message }); 
            }
        }
    }
}
