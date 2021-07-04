namespace BookList.Models
{
    /// <summary>
    /// Class that allows to initialize one instance of a book 
    /// </summary>
    public class Book
    {
        #region Properties
        /// <summary>
        /// Name of a book
        /// </summary>
        /// <value>Public property</value>
        public string Name { get; set; }
        /// <summary>
        /// Name of an author that wrote the book 
        /// </summary>
        /// <value>Public property</value>
        public string Author { get; set; } 
        /// <summary>
        /// Brief description of a book (max 255 characters)
        /// </summary>
        /// <value>Public property</value>
        public string Desciption { get; set; }
        #endregion  // Properties

        #region Constructors
        /// <summary>
        /// Defualt constructor of a book 
        /// </summary>
        public Book() { }

        /// <summary>
        /// Constructor that allows to initialize properties of the class 
        /// </summary>
        /// <param name="name">String value of a book's name</param>
        /// <param name="author">String value of a book's author</param>
        /// <param name="description">String value of a book's description</param>
        public Book(string name, string author, string description)
        {
            Name = name; 
            Author = author; 
            Desciption = description; 
        }
        #endregion  // Constructors
    }
}