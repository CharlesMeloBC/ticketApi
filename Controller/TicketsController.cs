using Microsoft.AspNetCore.Mvc;
using ticketApi.Models;
using ticketApi.Services;

namespace ticketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketsController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TicketDto>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();

            return Ok(tickets);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TicketModel>> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound();
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<ActionResult<TicketDto>> CreateTicket([FromBody] TicketDto ticketDto)
        {
            if (ticketDto == null || string.IsNullOrEmpty(ticketDto.Name))
                return BadRequest("Você precisa preencher os campos");

            var createdTicket = await _ticketService.CreateTicketAsync(ticketDto);

            return CreatedAtAction(nameof(GetTicketById), new { id = createdTicket.Id }, createdTicket);
        }

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
