using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims; 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication; 
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BookList.Models;
using BookList.Services; 

namespace BookList.Pages
{
    public class LoginModel : PageModel 
    {
        private readonly ILogger _logger; 

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger; 
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync(string fullname, string password)
        {
            // Get if input values are correct. 
            bool isFullnameCorrect = (fullname != string.Empty && fullname != null);
            bool isPasswordCorrect = (password != string.Empty && password != null);
            
            // Process fields. 
            if (isFullnameCorrect && isPasswordCorrect)
            {
                _logger.LogInformation($"{fullname} tries to log in."); 
                
                try
                {
                    // Verify the credentials.  
                    bool exists = Repository.UserRepositoryInstance.DoesExist(fullname, password); 
                    if (!exists)
                    {
                        throw new System.Exception($"User {fullname} does not exist in the DB."); 
                    }

                    // Creating the security context. 
                    var claims = new List<Claim> 
                    {
                        new Claim(ClaimTypes.Name, fullname), 
                        new Claim(ClaimTypes.Role, "User")
                    };
                    var identity = new ClaimsIdentity(claims, 
                        CookieAuthenticationDefaults.AuthenticationScheme); 
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = System.DateTimeOffset.UtcNow.AddMinutes(5),
                        IsPersistent = true
                    };
                    
                    // Sign in. 
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        new ClaimsPrincipal(identity), 
                        authProperties);
                    
                    string path = "../Books/Books"; 
                    
                    // Create new instance of the user in the repository. 
                    Repository.UserRepositoryInstance.AuthenticateUser(fullname); 
                    User user = Repository.UserRepositoryInstance.GetUser(); 
                    if (user == null || user.Fullname != fullname)
                    {
                        throw new System.Exception($"User {fullname} cannot be assigned in the UserRepository (instance is either null or empty)."); 
                    }

                    Repository.IsAuthenticated = true; 
                    _logger.LogInformation($"User {fullname} successfully logged in (redicted to {path})"); 

                    return RedirectToPage(path); 
                }
                catch (System.Exception e)
                {
                    _logger.LogWarning($"Exception: {e}"); 
                }
            }
            return Page(); 
        }
    }
}