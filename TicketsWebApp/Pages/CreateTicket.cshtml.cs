using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketsWebApp.Models;
using TicketsWebApp.Request;
using TicketsWebApp.Services;

namespace TicketsWebApp.Pages
{
    public class CreateTicketModel : PageModel
    {
        private readonly IApiService _apiService;

        public CreateTicketModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public CreateTicketRequest TicketRequest { get; set; }

        public IEnumerable<Provider> Providers { get; set; }

        public async Task OnGetAsync()
        {
            Providers = await _apiService.GetAllProvidersAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Providers = await _apiService.GetAllProvidersAsync();
                return Page();
            }

            var success = await _apiService.CreateTicketAsync(TicketRequest);
            if (success)
            {
                return RedirectToPage("Tickets");
            }

            ModelState.AddModelError(string.Empty, "Failed to create ticket.");
            Providers = await _apiService.GetAllProvidersAsync();
            return Page();
        }
    }
}
