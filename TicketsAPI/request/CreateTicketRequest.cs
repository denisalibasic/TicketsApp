namespace TicketsAPI.request
{
    public class CreateTicketRequest
    {
        public string? TicketName {  get; set; }
        public int TicketQuantity { get; set; }
        public string? ProviderName { get; set; }
    }
}
