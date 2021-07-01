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
        private IUserRepository _MockUserRepository; 

        private string Message { get; set; }

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger; 
            _MockUserRepository = new MockUserRepository(); 
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
                    if (user.Fullname == fullname && user.Password == password)
                    {
                        currentUser = user; 
                        break; 
                    }
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
                    _logger.LogInformation(Message); 
                }
            }
            else
            {
                Message = "OnPost method"; 
            }
        }
    }
}