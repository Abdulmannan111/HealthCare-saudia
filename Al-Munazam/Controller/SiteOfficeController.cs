using Application.DTOs;
using Application.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteOfficeController : ControllerBase
    {
        private readonly ISiteOfficeRepository _siteOfficeRepository;
        public SiteOfficeController(ISiteOfficeRepository siteOfficeRepository)
        {
            _siteOfficeRepository = siteOfficeRepository;
        }
        [HttpGet("GetSiteOffices")]
        public async Task<IActionResult> GetSiteOffices(int page, int per_page, string? search, string? region_id, string? facility, int? version, string? status)
        {
            try
            {
                var siteOffices = await _siteOfficeRepository.GetAllAsync(page, per_page, search, region_id, facility, version, status);
                return Ok(new { data = siteOffices });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetSiteOfficeById/{id}")]
        public async Task<IActionResult> GetSiteOfficeById(string id)
        {
            try
            {
                var siteOffice = await _siteOfficeRepository.GetByIdAsync(id);
                if (siteOffice == null)
                {
                    return NotFound();
                }
                return Ok(new { data = siteOffice });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateSiteOffice")]
        public async Task<IActionResult> CreateSiteOffice([FromBody] List<SiteOfficeDto> siteOfficeDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (siteOfficeDtos == null )
                return BadRequest("Office data is required.");

            try
            {
                await _siteOfficeRepository.CreateSiteOfficeAsync(siteOfficeDtos);
                return Ok(new { data = "Office Successfully Created" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateSiteOffice/{id}")]
        public async Task<IActionResult> UpdateSiteOffice(string id, [FromBody] SiteOfficeDto siteOfficeDto)
        {
            try
            {
                var existingSiteOffice = await _siteOfficeRepository.GetByIdAsync(id);
                if (existingSiteOffice == null)
                {
                    return NotFound();
                }
                await _siteOfficeRepository.UpdateSiteOfficeAsync(id, siteOfficeDto);
                return Ok(new { data = "Site Office Successfully Updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteSiteOffice/{id}")]
        public async Task<IActionResult> DeleteSiteOffice(string id)
        {
            try
            {
                var existingSiteOffice = await _siteOfficeRepository.GetByIdAsync(id);
                if (existingSiteOffice == null)
                {
                    return NotFound();
                }
                await _siteOfficeRepository.DeleteAsync(id);
                return Ok(new { data = "Site Office Successfully Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("export")]
        public async Task<IActionResult> export(int page, int per_page, string? search, string? region_id, string? facility, int? version, string? status)
        {
            try
            {
                var siteOffices = await _siteOfficeRepository.GetAllAsync(page, per_page, search, region_id, facility, version, status);
                return Ok(new { data = siteOffices });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
