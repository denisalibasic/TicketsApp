using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketsWebApp.Models;
using TicketsWebApp.Response;
using TicketsWebApp.Services;

namespace TicketsWebApp.Pages
{
    public class ProviderDetailsModel : PageModel
    {
        private readonly IApiService _apiService;

        public ProviderDetailsModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public Provider Provider { get; set; }
        public IEnumerable<ProviderTicketsResponse> Tickets { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var providerDetails = await _apiService.GetTicketsByProviderIdAsync(id);

            if (providerDetails == null)
            {
                return NotFound();
            }

            Provider = providerDetails.Provider;
            Tickets = providerDetails.Tickets;

            return Page();
        }
    }
}
