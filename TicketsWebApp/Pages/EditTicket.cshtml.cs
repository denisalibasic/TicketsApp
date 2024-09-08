using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketsWebApp.Models;
using TicketsWebApp.Request;
using TicketsWebApp.Services;

namespace TicketsWebApp.Pages
{
    public class EditTicketModel : PageModel
    {
        private readonly IApiService _apiService;

        public EditTicketModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public UpdateTicketRequest TicketRequest { get; set; }

        public int TicketId { get; set; }

        public IEnumerable<Provider> Providers { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ticket = await _apiService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            TicketId = ticket.Id;
            TicketRequest = new UpdateTicketRequest
            {
                Name = ticket.Name,
                Quantity = ticket.Quantity
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                Providers = await _apiService.GetAllProvidersAsync();
                TicketId = id;
                return Page();
            }

            var success = await _apiService.UpdateTicketAsync(id, TicketRequest);
            if (success)
            {
                return RedirectToPage("Tickets");
            }

            ModelState.AddModelError(string.Empty, "Failed to update ticket.");
            Providers = await _apiService.GetAllProvidersAsync();
            TicketId = id;
            return Page();
        }
    }
}
