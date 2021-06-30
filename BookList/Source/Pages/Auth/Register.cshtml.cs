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
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Get used";
        }
        
        public void OnPost()
        {
            string emailAddress = Request.Form["emailaddress"];
            Message = emailAddress; 
        }
    }
}