using BookList.Services; 

namespace BookList.Pages
{
    /// <summary>
    /// Static class for storing and accessing classes that implement 
    /// Repository pattern
    /// </summary>
    public static class Repository
    {
        #region Fields
        /// <summary>
        /// private static field for storing an instance of MockUserRepository. 
        /// </summary>
        private static IUserRepository userRepository = null;
        #endregion  // Fields

        #region Properties
        /// <summary>
        /// Property for getting an instance of UserRepository class. 
        /// </summary>
        /// <value>Public readonly property</value>
        public static IUserRepository UserRepositoryInstance
        {
            get 
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(); 
                }
                return userRepository;
            }
        }
        #endregion  // Properties
    }
}