using Microsoft.AspNetCore.Mvc;
using ticketApi.Models;
using ticketApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<TicketModel>>> GetAllTickets()
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
        public async Task<ActionResult<TicketModel>> CreateTicket([FromBody] TicketModel ticket)
        {
            if (ticket == null || string.IsNullOrEmpty(ticket.Name))
                return BadRequest("Você precisa preencher os campos");

            var createdTicket = await _ticketService.CreateTicketAsync(ticket);
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
