using System.Text.Json.Serialization;
using TicketsAPI.request;

namespace TicketsAPI.model
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int ProviderId { get; set; }
        [JsonIgnore]
        public Provider? Provider { get; set; }

        public Ticket(){}

        public Ticket(CreateTicketRequest ticketRequest, Provider provider) { 
            Name = ticketRequest.TicketName;
            Quantity = ticketRequest.TicketQuantity;
            ProviderId = provider.Id;
        }
    }
}
