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
        public string Message { get; private set; }

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
                Message = $"User {fullname} from {city} ({country}) tries to create an account"; 
            }
            else
            {
                Message = "OnPost method"; 
            }
        }
    }
}