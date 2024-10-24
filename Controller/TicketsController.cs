using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ticketApi.Models;
using ticketApi.Services;

namespace ticketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Authorize(Policy = "MicaDeveloperPolicy")]
        [HttpGet("ativos")]
        public async Task<ActionResult<List<TicketDto>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsyncActive();

            return Ok(tickets);
        }

        [Authorize(Policy = "MicaDeveloperPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicketById(int id)
        {
            var ticketDto = await _ticketService.GetTicketByIdAsync(id);
            if (ticketDto == null)
            return NotFound();
            return Ok(ticketDto);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<ActionResult<TicketDto>> GetAllTickets([FromQuery]bool allowAll = false)
        {
            var tickets = await _ticketService.GetAllTicketsAsync(true);
            return Ok(tickets);
        }

        [Authorize(Policy = "MicaDeveloperPolicy")]
        [HttpPost]
        public async Task<ActionResult<TicketDto>> CreateTicket([FromBody] TicketDto ticketDto)
        {
            if (ticketDto == null || string.IsNullOrEmpty(ticketDto.Name))
                return BadRequest("Você precisa preencher os campos");

            var createdTicket = await _ticketService.CreateTicketAsync(ticketDto);

            return CreatedAtAction(nameof(GetTicketById), new { id = createdTicket.Id }, createdTicket);
        }

        [Authorize(Policy = "MicaDeveloperPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var success = await _ticketService.DeleteTicketAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
