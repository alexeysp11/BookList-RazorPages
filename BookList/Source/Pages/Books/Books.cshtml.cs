using Microsoft.AspNetCore.Mvc;
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

        public void OnGet()
        {
            try
            {
                Repository.UserRepositoryInstance.GetBooksFromDb(); 
                var books = Repository.UserRepositoryInstance.GetBookList(); 
                if (books == null)
                {
                    throw new System.Exception("List of books cannot be null"); 
                }
            }
            catch (System.Exception e)
            {
                _logger.LogWarning($"Exception in OnGet method: {e}"); 
            }
        }

        public IActionResult OnPostAddNewBook()
        {
            _logger.LogInformation($"User {Repository.UserRepositoryInstance.GetUser().Fullname} wants to add new book."); 
            return RedirectToPage("AddBook"); 
        }
    }
}