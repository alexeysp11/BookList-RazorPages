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
        MockUserRepository _MockUserRepository; 

        private string Message { get; set; }

        public RegisterModel(ILogger<RegisterModel> logger)
        {
            _logger = logger; 
        }

        public void OnGet()
        {
            Message = "Get used";
        }
        
        public IActionResult OnPost(string fullname, string country, string city, 
            string password)
        {
            // Get if input values are correct. 
            bool isFullnameCorrect = (fullname != string.Empty && fullname != null);
            bool isCountryCorrect = (country != string.Empty && country != null);
            bool isCityCorrect = (city != string.Empty && city != null);
            bool isPasswordCorrect = (password != string.Empty && password != null);

            _MockUserRepository = new MockUserRepository(); 

            // Process fields. 
            if (isFullnameCorrect && isCountryCorrect && isCityCorrect && 
                isPasswordCorrect)
            {
                // Log information that user tries to create an account. 
                Message = $"User {fullname} ({city}, {country}) tries to create an account."; 
                _logger.LogInformation(Message);

                // Get an instance of current user. 
                User currentUser; 
                try
                {
                    currentUser = GetCurrentUser(fullname, country, city, password); 
                    _logger.LogInformation($"User {fullname} ({city}, {country}) created an account successfully."); 
                }
                catch (System.Exception e)
                {
                    _logger.LogWarning($"Exception: {e}"); 
                    return RedirectToPage(); 
                }
                Repository.MockUserRepositoryInstance = _MockUserRepository; 
                _MockUserRepository = null; 
                return RedirectToPage("Login"); 
            }
            else
            {
                Message = string.Empty; 
            }
            _MockUserRepository = null; 
            return RedirectToPage(); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        private User GetCurrentUser(string fullname, string country, string city, 
            string password)
        {
            User currentUser = null; 
            try
            {
                _MockUserRepository.CreateUser(fullname, country, city, 
                    password); 
                currentUser = _MockUserRepository.GetUser(fullname, 
                    password); 
            }
            catch (System.Exception e)
            {
                throw e; 
            }

            return currentUser;
        }
    }
}