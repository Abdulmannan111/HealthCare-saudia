using Application.Entities;
using Application.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Al_Munazam.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketSlasController : ControllerBase
    {
        private readonly ITicketSlasRepository _ticketSlasRepository;

        public TicketSlasController(ITicketSlasRepository ticketSlasRepository)
        {
            _ticketSlasRepository = ticketSlasRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page, int per_page, string? search)
        {
            var ticketSlas = await _ticketSlasRepository.GetAllAsync(page,per_page,search);
            return Ok(new { data = ticketSlas });
        }
        [HttpGet("export")]
        public async Task<IActionResult> export(int page, int per_page, string? search)
        {
            var ticketSlas = await _ticketSlasRepository.GetAllAsync(page,per_page,search);
            return Ok(new { data = ticketSlas });
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TicketSlas>> GetById(string id)
        {
            var ticketSla = await _ticketSlasRepository.GetByIdAsync(id);
            if (ticketSla == null)
                return NotFound();
            return Ok(new { data = ticketSla });
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] TicketSlas ticketSla)
        {
            try
            {
                await _ticketSlasRepository.CreateAsync(ticketSla);
                return Ok(new { data = "Ticket Successfully Created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] TicketSlas ticketSla)
        {
            var existing = await _ticketSlasRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _ticketSlasRepository.UpdateAsync(id, ticketSla);
            return Ok(new { data = "Ticket Successfully Updated" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existing = await _ticketSlasRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _ticketSlasRepository.DeleteAsync(id);
            return Ok(new { data = "Ticket Successfully Deleted" });

        }
    }
}
