namespace ticketApi.Models
{
    public class TicketModel
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = null!;

        public StatusType Status { get; private set; }

        public DateTime Created { get; private set; }

        public bool IsActive { get; private set; }

        public string? Role {  get; private set; }

        public DateTime? DeletedAT { get; private set; }

        public DateTime? DisplayDeletedAt => DeletedAT;

        public  string Description { get; internal set; } = string.Empty;

        private TicketModel()
        {
        }

        public TicketModel(string name, string description)
        {
            Name = name;
            Status = StatusType.Open;
            Created = DateTime.Now;
            Description = description;
            IsActive = true;
        }

        public void Disable()
        {
            DeletedAT = DateTime.Now;
            IsActive = false;
        }
    }
}