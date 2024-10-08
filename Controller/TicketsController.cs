using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketApi.Data;
using ticketApi.Models;


namespace ticketApi.Controller
{
    [Route("api[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController (AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TiketModel>>> GetAllTickets()
        {
            var tickets = await _context.Tikets.ToListAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TiketModel>> GetTicketById(int id)
        {
            var tickets = await _context.Tikets.FindAsync(id);
            if (tickets == null) 
                return NotFound();
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<ActionResult<TiketModel>> CreateTicket([FromBody] TiketModel ticket)
        {
            if (ticket == null || string.IsNullOrEmpty(ticket.Name))
                return BadRequest("Você precisa preencher os campos");
            _context.Tikets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateTicket), new {id = ticket.Id }, ticket);

        }
    }
}
