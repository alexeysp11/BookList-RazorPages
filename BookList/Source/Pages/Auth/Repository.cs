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
        private static MockUserRepository mockUserRepository;
        #endregion  // Fields

        #region Properties
        /// <summary>
        /// Property for getting and setting an instance of MockUserRepository class. 
        /// </summary>
        /// <value>Public for getting and setting (sets only if private field
        /// is null)</value>
        public static MockUserRepository MockUserRepositoryInstance
        {
            get { return mockUserRepository; }
            set 
            {
                if (mockUserRepository == null)
                {
                    mockUserRepository = value; 
                }
            }
        }
        #endregion  // Properties
    }
}