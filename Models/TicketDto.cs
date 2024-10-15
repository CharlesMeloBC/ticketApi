namespace ticketApi.Models
{
    public class TicketDto
    {
        public int? Id { get; set; }

        public string Name { get; set; } = null!;

        public StatusType? Status { get; set; }

        public DateTime? Created { get; set; }

        public bool? IsActive { get; set; }
    }

    public class TicketDtoAllInfo
    {
        public int? Id { get; set; }

        public string Name { get; set; } = null!;

        public StatusType? Status { get; set; }

        public DateTime? Created { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? DeletedAT { get; set; }
    }


}
