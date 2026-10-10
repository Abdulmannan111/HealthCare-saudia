using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Domain.Repositories;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitySiteController : ControllerBase
    {
        private readonly IFacilitySiteRepository _facilitySiteRepository;
        private readonly IActivityLogRepository _modificationHistoryRepository;
        private readonly IUserRepository _userRepository;


        public FacilitySiteController(IFacilitySiteRepository facilitySiteRepository, IActivityLogRepository modificationHistoryRepository, IUserRepository userRepository)
        {
            _facilitySiteRepository = facilitySiteRepository;
            _modificationHistoryRepository = modificationHistoryRepository;
            _userRepository = userRepository;
        }

        [HttpGet("GetFacilitySiteById/{id}")]
        public async Task<IActionResult> GetFacilitySiteById(string id)
        {          
            try
            {
                var site = await _facilitySiteRepository.GetFacilitySiteByIdAsync(id);
                if (site == null)
                {
                    return NotFound();
                }
                return Ok(new { data = site });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetParentSites/{regionid}/{clusterid}")]
        public async Task<IActionResult> GetParentSites(
    string regionid,
    string clusterid,
    [FromQuery] string? search = null,
    [FromQuery] int skip = 0,
    [FromQuery] int take = 20)
        {
            try
            {
                var sites = await _facilitySiteRepository.GetParentSiteAsync(regionid, clusterid, search, skip, take);
                return Ok(new { data = sites });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("CreateFacilitySite")]
        public async Task<IActionResult> CreateFacilitySite(ObligationSitesDto site)
        {
            if (site == null)
            {
                return BadRequest("Site data is required.");
            }

            try
            {
                await _facilitySiteRepository.CreateFacilitySiteAsync(site);
            return Ok(new { data = "Site Successfully Created" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ApproveSite(string id)
        {
            var siteData = await _facilitySiteRepository.GetByIdAsync(id);
            if (siteData == null)
            {
                return NotFound(new { data = "Site not found!" });
            }

            var user = await _userRepository.GetUserFromHttpContextAsync();
            if (!(user.IsAdmin() || user.IsRegional() || user.IsSupervisor()))
            {
                return Unauthorized(new { data = "You are not authorized to change the status!" });
            }

            siteData.status = "active";
            await _facilitySiteRepository.UpdateAsync(id, siteData);

            return Ok(new { data = "Site Approved Successfully" });
        }

        [HttpGet("GetAllFacilitySites")]
        public async Task<ActionResult<IEnumerable<ObligationSites>>> GetAllFacilitySites(
    int page = 0, int per_page = 10, string? search = null,
    string? region_id = null, string? facility = null,
    int version = 5, string status = "ACTIVE", string submission_for =null)
        {
            try
            {
                // Normalize status value
                status = status.Equals("inactive", StringComparison.OrdinalIgnoreCase) ? "in_active" : status.ToLower();

                var sites = await _facilitySiteRepository.GetAllFacilitySitesAsync(page, per_page, search, region_id, facility, version, status, submission_for);

                return Ok(new { data = sites, resultCount = sites.resultCount_ });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



        [HttpGet("export")]
        public async Task<ActionResult<IEnumerable<ObligationSites>>> export(int page = 0, int per_page = 10, string? search = null, string? region_id = null, string? facility = null, int version = 5, string status = "ACTIVE", string submission_for = null)
        {
            try
            {
                
                var sites = await _facilitySiteRepository.GetAllFacilitySitesAsync(page, per_page, search, region_id, facility, version, status, submission_for);
                var data = sites;
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateFacilitySite/{id}")]
        public async Task<IActionResult> UpdateFacilitySite(string id, ObligationSitesDto site)
        {         
            try
            {
                var existingSite = await _facilitySiteRepository.GetFacilitySiteByIdAsync(id);

                var changes = $"Updated FacilitySite {id}: " +
                        $"FacilityType from {existingSite.facilityType} to {existingSite.facilityType}, ";


                if (existingSite == null)
                {
                    return NotFound();
                }
                await _facilitySiteRepository.UpdateFacilitySiteAsync(id,site);

                var history = new ActivityLog
                {
                    UserId = existingSite.name,
                    LogDate = DateTime.UtcNow,
                    LogDescription = changes
                };

                await _modificationHistoryRepository.LogModificationAsync(history);
                return Ok(new { data = "Site Updated Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var site = await _facilitySiteRepository.GetFacilitySiteByIdAsync(id);
                if (site == null)
                {
                    return NotFound();
                }

                await _facilitySiteRepository.DeleteAsync(id);
                return Ok(new { data = "Site Successfully Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("log-modification")]
        public async Task<IActionResult> LogModification([FromBody] ActivityLog history)
        {
            await _modificationHistoryRepository.LogModificationAsync(history);
            return NoContent();
        }
    }
}
