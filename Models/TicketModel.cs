namespace ticketApi.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public enum Status { open, close }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
