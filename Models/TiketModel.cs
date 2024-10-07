namespace ticketApi.Models
{
    public class TiketModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool status { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
