namespace TicketsWebApp.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string? TicketName { get; set; }
        public int TicketQuantity { get; set; }
        public int ProviderId { get; set; }
        public string? ProviderName { get; set; }
    }
}
