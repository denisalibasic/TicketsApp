using TicketsWebApp.Response;

namespace TicketsWebApp.Models
{
    public class ProviderDetails
    {
        public Provider? Provider { get; set; }
        public IEnumerable<ProviderTicketsResponse>? Tickets { get; set; }
    }
}
