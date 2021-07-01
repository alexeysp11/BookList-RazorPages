using System.Collections.Generic; 
using BookList.Models; 

namespace BookList.Services
{
    public interface IUserRepository 
    {
        User GetUser(); 
        List<User> GetUsers(); 
    }
}