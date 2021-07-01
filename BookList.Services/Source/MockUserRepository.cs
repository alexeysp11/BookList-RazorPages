using System.Collections.Generic; 
using BookList.Models; 

namespace BookList.Services
{
    public class MockUserRepository : IUserRepository 
    {
        #region Private fields
        /// <summary>
        /// List of current users. 
        /// </summary>
        /// <typeparam name="User"></typeparam>
        /// <returns></returns>
        private List<User> Users = new List<User>(); 
        /// <summary>
        /// Instance of the current user. 
        /// </summary>
        private User UserObj = null; 
        #endregion  // Private fields

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
        #endregion  // Constructors

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullname">String value of fullname of the user</param>
        /// <param name="country">String value of country of the user</param>
        /// <param name="city">String value of city of the user</param>
        /// <param name="password">String value of password of the user</param>
        public void CreateUser(string fullname, string country, string city, 
            string password)
        {
            // Get if such user already exists (if yes, just return). 

            // Add user. 
            this.Users.Add(new User() 
            {
                Fullname = fullname, 
                Country = country, 
                City = city, 
                Password = password
            });
        }

        /// <summary>
        /// Allows to get current active user. 
        /// </summary>
        /// <param name="fullname">String value of fullname of the user</param>
        /// <param name="password">String value of password of the user</param>
        /// <returns>Instance of User class</returns>
        public User GetUser(string fullname, string password)
        {
            User currentUser = null; 

            // Find user in the list of users. 
            foreach (var user in Users)
            {
                try
                {
                    if (user.Fullname == fullname && user.Password == password)
                    {
                        currentUser = user; 
                        break; 
                    }
                }
                catch (System.Exception e)
                {
                    throw e;
                }
            }

            if (currentUser == null)
            {
                throw new System.ArgumentNullException("User cannot be null"); 
            }

            return currentUser; 
        }

        /// <summary>
        /// Allows to get current active user. 
        /// </summary>
        /// <returns>Instance of User class</returns>
        public User GetUser()
        {
            return UserObj; 
        }
        #endregion  // Methods
    }
}