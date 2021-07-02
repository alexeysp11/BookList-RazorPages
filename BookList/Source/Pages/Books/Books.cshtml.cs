using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookList.Pages
{
    public class BooksModel : PageModel 
    {
        private readonly ILogger _logger; 

        public BooksModel(ILogger<BooksModel> logger)
        {
            _logger = logger; 
        }
    }
}