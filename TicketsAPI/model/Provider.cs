namespace TicketsAPI.model
{
    public class Provider
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
