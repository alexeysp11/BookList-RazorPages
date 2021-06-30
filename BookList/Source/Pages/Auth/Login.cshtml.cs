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
        public string Greeting { get; set; } = "Hello"; 

        public void OnGet()
        {

        }
    }
}