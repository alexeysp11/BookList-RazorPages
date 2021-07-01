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
    public class LoginModel : PageModel 
    {
        private readonly ILogger _logger; 

        private string Message { get; set; }

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger; 
        }

        public void OnGet()
        {
            
        }

        public void OnPost(string fullname, string password)
        {
            // Get if input values are correct. 
            bool isFullnameCorrect = (fullname != string.Empty && fullname != null);
            bool isPasswordCorrect = (password != string.Empty && password != null);
            
            // Process fields. 
            if (isFullnameCorrect && isPasswordCorrect)
            {
                // Log information that user tries to login. 
                Message = $"{fullname} tries to log in."; 
                _logger.LogInformation(Message); 

                MockUserRepository _MockUserRepository = Repository.MockUserRepositoryInstance; 

                // Get user inside MockUserRepository. 
                User currentUser = null; 
                try
                {    
                    currentUser = _MockUserRepository.GetUser(fullname, password); 
                }
                catch (System.Exception e)
                {
                    _logger.LogWarning($"Exception: {e}"); 
                } 

                // Log information. 
                if (currentUser != null)
                {
                    Message = $"{currentUser.Fullname} logged in successfully."; 
                    _logger.LogInformation(Message); 
                }
                else
                {
                    Message = $"User {fullname} was not found."; 
                    _logger.LogWarning(Message); 
                }
            }
            else
            {
                Message = string.Empty; 
            }
        }
    }
}