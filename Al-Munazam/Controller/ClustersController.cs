using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClustersController : ControllerBase
    {
        private readonly IClusterRepository _clusterRepository;
        private readonly IConfiguration Configuration;

        public ClustersController(IClusterRepository clusterRepository, IConfiguration configuration)
        {
            _clusterRepository = clusterRepository;
            Configuration = configuration;
        }

        [HttpGet("GetAllClusters")]
        public async Task<ActionResult<IEnumerable<Clusters>>> GetAll(int page = 1, int per_page = 10, string? search = null, string submission_for = null)
        {
            try
            {
                // Retrieve clusters
                var clusters = await _clusterRepository.GetAllAsync(page, per_page, search, submission_for);
                var data = new
                {
                    data = clusters
                };
                return Ok(data);
            }
            catch (SecurityTokenException ex)
            {
                // Token validation failed
                return Unauthorized(new { message = "Invalid or expired token", error = ex.Message });
            }
        }



        [HttpGet("export")]
        public async Task<ActionResult<IEnumerable<Clusters>>> export(int page = 1, int per_page = 10, string? search = null, string submission_for = null)
        {
            var clusters = await _clusterRepository.GetAllAsync(page, per_page, search, submission_for);
            var data = clusters;
            return Ok(data);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Clusters>> Get(string id)
        {
            try
            {
                var cluster = _clusterRepository.GetById(id);
                if (cluster == null)
                {
                    return NotFound();
                }
                var data = new
                {
                    data = cluster
                };
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }       
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ClusterDto cluster)
        {
            if (cluster == null)
            {
                return BadRequest("Cluster data is required.");
            }

            try
            {
                await _clusterRepository.CreateAsync(cluster);
                return Ok(new { data = "Cluster Successfully Created" });
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
        public async Task<IActionResult> Update(string id, ClusterDto cluster)
        {
            try
            {
                var existingCluster = _clusterRepository.GetById(id);
                if (existingCluster == null)
                {
                    return NotFound();
                }
                await _clusterRepository.UpdateAsync(id, cluster);
                return Ok(new { data = "Cluster Updated Successfully" });
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
                var cluster = _clusterRepository.GetById(id);
                if (cluster == null)
                {
                    return NotFound();
                }

                await _clusterRepository.DeleteAsync(id);
                return Ok(new { data = "Cluster Successfully Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
