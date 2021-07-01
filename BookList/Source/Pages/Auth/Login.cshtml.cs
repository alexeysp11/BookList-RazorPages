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
        public string Message { get; private set; } = "Default message"; 

        public void OnGet()
        {
            Message = "Default message"; 
        }

        public void OnPost(string fullname, string password)
        {
            // Get if input values are correct. 
            bool isFullnameCorrect = (fullname != string.Empty && fullname != null);
            bool isPasswordCorrect = (password != string.Empty && password != null);
            
            // Process fields. 
            if (isFullnameCorrect && isPasswordCorrect)
            {
                Message = $"{fullname} tries to log in"; 
            }
            else
            {
                Message = "OnPost method"; 
            }
        }
    }
}