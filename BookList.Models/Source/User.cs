namespace BookList.Models
{
    /// <summary>
    /// Model of an authenticated User 
    /// </summary>
    public class User
    {
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
    }
}