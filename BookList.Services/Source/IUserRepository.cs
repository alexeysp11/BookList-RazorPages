using System.Collections.Generic; 
using BookList.Models; 

namespace BookList.Services
{
    public interface IUserRepository 
    {
        void CreateUser(string fullname, string country, string city, 
            string password); 
        bool DoesExist(string fullname, string password); 
        void AuthenticateUser(string fullname, string password); 
        void LogOutUser(); 
        User GetUser(); 

        void AddNewBook(string name, string author, string description); 
        List<Book> GetBookList(); 
        void GetBooksFromDb(); 
    }
}