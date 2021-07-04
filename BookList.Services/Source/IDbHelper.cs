using System.Collections.Generic; 
using BookList.Models; 

namespace BookList.Services
{
    /// <summary>
    /// Interface that allows to implement any database helper 
    /// </summary>
    public interface IDbHelper
    {
        void CreateTables(); 

        void Insert(string request); 
        void Insert(string insertRequest, string checkRequest); 

        void Update(string request); 

        bool DoesExist(string readRequest); 
        void GetInfoAboutUser(string fullname, out string country, 
            out string city, string password); 
        
        List<Book> GetBooksFromDb(string readRequest); 
    }
}