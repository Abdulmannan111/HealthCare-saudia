using Microsoft.AspNetCore.Mvc;
using Application.IRepositories;
using Application.DTOs.RiskCompliance;
using Application.Entities.RiskCompliance;

namespace Al_Munazam.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RIskComplianceController : ControllerBase
    {
        private readonly IRiskComplianceItem _risk;

        public RIskComplianceController(IRiskComplianceItem risk)
        {
            _risk = risk;
        }

        [HttpGet("show/{id}")]
        public async Task<ActionResult<RiskComplianceItem>> GetById(string id)
        {
            try
            {
                var riskCompliance = await _risk.GetByIdAsync(id);
               // var activty = await _risk.activity();
                if (riskCompliance == null)
                {
                    return NotFound();
                }
                var data = new 
                {
                    data =riskCompliance,
                };
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("RiskCompliance")]
        public async Task<ActionResult<IEnumerable<RiskComplianceItem>>> GetAll(int page = 1, int per_page = 10, string? search = null, string? deleted_at = null)
        {
            try
            {
                var riskCompliance = await _risk.GetAllAsync(page, per_page, search, deleted_at);
               // var activity = await 
                var resultCount = await _risk.GetCounts();
                var data = new
                {
                    data = riskCompliance,
                    resultCount = resultCount
                };
                return Ok(new { data = data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RiskComplianceRequestDto risk)
        {
            if (risk == null)
            {
                return BadRequest("RiskCompliance data is required.");
            }

            try
            {
                var data = new RiskComplianceItemDto
                {
                    _id = risk.id,
                    category_id = risk.category.ToString(),
                    domain_id = risk.healthcareDomain.ToString(),
                    identifier = risk.identifier,
                    impact = (double?)risk.impact,
                    legal_descriptions = risk.legal_descriptions,
                    likelihood = (double?)risk.likelihood,
                    impact_prefix = (int)risk.prefixImpact,
                    risk = (double)risk.risk,
                    score = (int)risk.score,
                    standard = risk.standard,
                    subCategory_id = risk.subCategory.ToString(),
                    weightage = (double)risk.weightage,
                    deleted_at = null
                };
                await _risk.CreateAsync(data);
                return Ok(new { data = "RiskCompliance Successfully Created" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] RiskComplianceRequestDto risk)
        {
            try
            {
                var existingRiskCompliance = await _risk.GetByIdAsync(id);
                if (existingRiskCompliance == null)
                {
                    return NotFound();
                }
                await _risk.UpdateAsync(id, risk);
                return Ok(new { data = "RiskCompliance Updated Successfully" });
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
                var existingRiskCompliance = await _risk.GetByIdAsync(id);
                if (existingRiskCompliance == null)
                {
                    return NotFound();
                }

                await _risk.DeleteAsync(id);
                return Ok(new { data = "RiskCompliance Successfully Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportRiskComplianceAsync(int page = 1, int pageSize = 20000, string? search = null, string? regionId = null)
        {
            var memoryStream = await _risk.ExportToExcelAsync(page, pageSize, search, regionId);

            // Set the file name and return the file in the response
            string fileName = "RiskComplianceExport.xlsx";
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        [HttpGet("categories")]
        public async Task<IActionResult> categories()
        {
            try
            {
                var category = await _risk.categories();
               
                return Ok(new { category[0].category, category[0].domain });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
