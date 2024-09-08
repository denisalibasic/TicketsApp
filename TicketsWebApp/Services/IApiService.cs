using TicketsWebApp.Models;
using TicketsWebApp.Request;
using TicketsWebApp.Response;

namespace TicketsWebApp.Services
{
    public interface IApiService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<EditTicketResponse> GetTicketByIdAsync(int id);
        Task<bool> CreateTicketAsync(CreateTicketRequest ticket);
        Task<bool> UpdateTicketAsync(int id, UpdateTicketRequest ticket);
        Task<bool> DeleteTicketAsync(int id);
        Task<IEnumerable<Provider>> GetAllProvidersAsync();
        Task<Provider> GetProviderByIdAsync(int id);
        Task<ProviderDetails> GetTicketsByProviderIdAsync(int providerId);
    }
}
