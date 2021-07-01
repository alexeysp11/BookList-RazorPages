using System.Collections.Generic; 
using BookList.Models; 

namespace BookList.Services
{
    public interface IUserRepository 
    {
        void CreateUser(string fullname, string country, string city, 
            string password); 
        User GetUser(); 
    }
}