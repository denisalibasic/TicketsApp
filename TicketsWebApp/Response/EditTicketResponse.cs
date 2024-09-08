namespace TicketsWebApp.Response
{
    public class EditTicketResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int ProviderId { get; set; }
    }
}
