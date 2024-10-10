namespace ticketApi.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public enum StatusType
        {
            Open,
            Close
        }

        public StatusType Status { get; set; } = StatusType.Open;

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime? DeletedAT { get; set; }
    }
}
