using TicketsWebApp.Models;
using TicketsWebApp.Request;
using TicketsWebApp.Response;

namespace TicketsWebApp.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            var response = await _httpClient.GetAsync("/tickets");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>();
        }

        public async Task<EditTicketResponse> GetTicketByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/tickets/{id}");
            return await response.Content.ReadFromJsonAsync<EditTicketResponse>();
        }

        public async Task<bool> CreateTicketAsync(CreateTicketRequest ticket)
        {
            var response = await _httpClient.PostAsJsonAsync("/tickets", ticket);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTicketAsync(int id, UpdateTicketRequest ticket)
        {
            var response = await _httpClient.PutAsJsonAsync($"/tickets/{id}", ticket);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/tickets/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Provider>> GetAllProvidersAsync()
        {
            var response = await _httpClient.GetAsync("/providers");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Provider>>();
        }

        public async Task<Provider> GetProviderByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/provider/{id}");
            return await response.Content.ReadFromJsonAsync<Provider>();
        }

        public async Task<ProviderDetails> GetTicketsByProviderIdAsync(int providerId)
        {
            var response = await _httpClient.GetAsync($"/tickets/provider/{providerId}");
            return await response.Content.ReadFromJsonAsync<ProviderDetails>();
        }
    }
}
