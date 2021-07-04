using System.Collections.Generic; 

namespace BookList.Models
{
    /// <summary>
    /// Model of an authenticated User 
    /// </summary>
    public class User
    {
        #region Personal information
        /// <summary>
        /// Fullname of an authenticated user. 
        /// </summary>
        /// <value>Public property</value>
        public string Fullname { get; set; }
        /// <summary>
        /// Nationality of a user. 
        /// </summary>
        /// <value>Public property</value>
        public string Country { get; set; }
        /// <summary>
        /// City where the user lives in. 
        /// </summary>
        /// <value>Public property</value>
        public string City { get; set; }
        #endregion  // Personal information

        #region Interests 
        /// <summary>
        /// List of the books that user reads 
        /// </summary>
        /// <value>Public property</value>
        public List<Book> Books {get; set; } 
        #endregion  // Interests 
    }
}