using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketsWebApp.Models;
using TicketsWebApp.Services;

namespace TicketsWebApp.Pages
{
    public class ProvidersModel : PageModel
    {
        private readonly IApiService _apiService;

        public ProvidersModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public IEnumerable<Provider> Providers { get; set; }

        public async Task OnGetAsync()
        {
            Providers = await _apiService.GetAllProvidersAsync();
        }
    }
}
