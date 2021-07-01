using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookList.Models;
using BookList.Services; 

namespace BookList.Pages
{
    public class RegisterModel : PageModel 
    {
        private readonly ILogger _logger; 
        private IUserRepository _MockUserRepository; 

        private string Message { get; set; }

        public RegisterModel(ILogger<RegisterModel> logger)
        {
            _logger = logger; 
        }

        public void OnGet()
        {
            Message = "Get used";
        }
        
        public void OnPost(string fullname, string country, string city, 
            string password)
        {
            // Get if input values are correct. 
            bool isFullnameCorrect = (fullname != string.Empty && fullname != null);
            bool isCountryCorrect = (country != string.Empty && country != null);
            bool isCityCorrect = (city != string.Empty && city != null);
            bool isPasswordCorrect = (password != string.Empty && password != null);

            // Process fields. 
            if (isFullnameCorrect && isCountryCorrect && isCityCorrect && 
                isPasswordCorrect)
            {
                // Log information that user tries to create an account. 
                Message = $"User {fullname} ({city}, {country}) tries to create an account."; 
                _logger.LogInformation(Message);

                // Initialize an object of MockUserRepository. 
                _MockUserRepository = new MockUserRepository(fullname, country, city, password); 

                // Get list of users inside MockUserRepository. 
                User currentUser = null; 
                List<User> users = _MockUserRepository.GetUsers(); 
                if (users == null)
                {
                    Message = "No users in the MockUserRepository"; 
                    _logger.LogInformation(Message); 
                    return; 
                }

                // Find user in the list of users. 
                foreach (var user in users)
                {
                    if (user.Fullname == fullname && user.Country == country 
                        && user.City == city && user.Password == password)
                    {
                        currentUser = user; 
                        break; 
                    }
                }

                // Log information. 
                if (currentUser != null)
                {
                    Message = $"{currentUser.Fullname} created an account successfully."; 
                    _logger.LogInformation(Message); 
                }
                else
                {
                    Message = $"Error while getting back data from DB: User {fullname} was not found in the response."; 
                    _logger.LogInformation(Message); 
                }
            }
            else
            {
                Message = string.Empty; 
            }
        }
    }
}