using ticketApi.Models;

namespace ticketApi.Services
{
    public interface ITicketService
    {
        Task<List<TicketDto>> GetAllTicketsAsyncActive();
        Task<List<TicketDto>> GetAllTicketsAsync(bool includeDeleted = false);
        Task<TicketDto?> GetTicketByIdAsync(int id);
        Task<TicketDto> CreateTicketAsync(TicketDto ticketDto);
        Task<bool> DeleteTicketAsync(int id);
    }
}
