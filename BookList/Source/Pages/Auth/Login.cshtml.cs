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

                MockUserRepository _MockUserRepository; 
                if (Repository.MockUserRepositoryInstance != null)
                {
                    _MockUserRepository = Repository.MockUserRepositoryInstance; 
                }
                else
                {
                    _MockUserRepository = new MockUserRepository(); 
                    Repository.MockUserRepositoryInstance = _MockUserRepository; 
                }

                // Get user inside MockUserRepository. 
                try
                {    
                    bool exists = _MockUserRepository.DoesExist(fullname, password); 
                    if (!exists)
                    {
                        throw new System.Exception($"User {fullname} does not exist after inserting into the DB."); 
                    }
                    _logger.LogWarning($"User {fullname} successfully logged in."); 
                }
                catch (System.Exception e)
                {
                    _logger.LogWarning($"Exception: {e}"); 
                } 
            }
            else
            {
                Message = string.Empty; 
            }
        }
    }
}