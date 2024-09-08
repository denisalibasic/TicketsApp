using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketsWebApp.Models;
using TicketsWebApp.Services;

namespace TicketsWebApp.Pages
{
    public class TicketsModel : PageModel
    {
        private readonly IApiService _apiService;

        public TicketsModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public IEnumerable<Ticket> Tickets { get; set; }

        public async Task OnGetAsync()
        {
            Tickets = await _apiService.GetAllTicketsAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (await _apiService.DeleteTicketAsync(id))
            {
                return RedirectToPage();
            }
            return Page();
        }
    }
}
