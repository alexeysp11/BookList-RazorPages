using System.Collections.Generic; 
using BookList.Models; 

namespace BookList.Services
{
    public class MockUserRepository : IUserRepository 
    {
        public List<User> Users { get; private set; } = new List<User>(); 
        private User UserObj { get; set; } = null; 

        #region Constructors
        /// <summary>
        /// Constructor that initilizes the user by default. 
        /// </summary>
        public MockUserRepository()
        {
            this.Users.Add(new User() 
            {
                Fullname = "DefaultUser", 
                Country = "England", 
                City = "Manchester",  
                Password = "DefaultPassword"
            });
        }

        /// <summary>
        /// Constructor that initializes the user by input parameters. 
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <param name="password"></param>
        public MockUserRepository(string fullname, string country, string city, 
            string password)
        {
            this.Users.Add(new User() 
            {
                Fullname = fullname, 
                Country = country, 
                City = city,  
                Password = password
            });
        }
        #endregion  // Constructors

        public List<User> GetUsers()
        {
            return Users; 
        }

        public User GetUser()
        {
            return UserObj; 
        }
    }
}