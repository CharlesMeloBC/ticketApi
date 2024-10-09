namespace ticketApi.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
