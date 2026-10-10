using Microsoft.AspNetCore.Mvc;
using Application.IRepositories;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly ISiteRepository _siteService;

        public SiteController(ISiteRepository siteService)
        {
            _siteService = siteService;
        }

       

        [HttpGet("GetSite")]
        public async Task<IActionResult> GetSite(string id)
        {
            var data =  await _siteService.GetById(id);        
            return Ok(data);
        }

        private string GenerateLicenseNumber(string regionId, string clusterId, string licenseNumber)
        {
            return $"{regionId.Substring(0, 2)}{clusterId.Substring(0, 2)}{licenseNumber}";
        }
    }
}
