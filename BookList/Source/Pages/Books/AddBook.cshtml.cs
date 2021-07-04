using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookList.Models; 
using BookList.Services; 

namespace BookList.Pages
{
    public class AddBookModel : PageModel
    {
        private readonly ILogger _logger; 

        public AddBookModel(ILogger<AddBookModel> logger)
        {
            _logger = logger; 
        }

        public IActionResult OnPostAddBtn(string name, string author, string year, 
            string description)
        {
            // Get if input values are correct. 
            bool isNameCorrect = (name != null && name != string.Empty);
            bool isAuthorCorrect = (author != null && author != string.Empty);
            bool isYearCorrect = (year != null && year != string.Empty);
            bool isDescriptionCorrect = (description != null && description != string.Empty);

            // Process fields. 
            if (isNameCorrect && isAuthorCorrect && isYearCorrect && 
                isDescriptionCorrect)
            {
                Repository.UserRepositoryInstance.AddNewBook(name, author, description); 
                
                _logger.LogInformation($"Book is added by {Repository.UserRepositoryInstance.GetUser().Fullname}."); 
                return RedirectToPage("Books");
            }
            return RedirectToPage(); 
        }
    }
}