namespace ticketApi.Models
{
    public class TicketModel
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = null!;

        public StatusType Status { get; private set; }

        public DateTime Created { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime? DeletedAT { get; private set; }

        public DateTime? DisplayDeletedAt => DeletedAT;

        private TicketModel()
        {
        }

        public TicketModel(string name)
        {
            Name = name;
            Status = StatusType.Open;
            Created = DateTime.Now;
            IsActive = true;
        }

        public void Disable()
        {
            DeletedAT = DateTime.Now;
            IsActive = false;
        }
    }
}