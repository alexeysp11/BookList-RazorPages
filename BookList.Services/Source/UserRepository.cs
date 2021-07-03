using System.Collections.Generic; 
using BookList.Models; 
using Security.Models; 

namespace BookList.Services
{
    public class UserRepository : IUserRepository 
    {
        #region Members
        /// <summary>
        /// Instance of database helper for interacting with SQLite DB  
        /// </summary>
        private SqliteDbHelper DbHelper; 
        #endregion  // Members

        #region Private fields
        /// <summary>
        /// Instance of the current user. 
        /// </summary>
        private User UserObj = null; 
        #endregion  // Private fields

        #region Constructors
        /// <summary>
        /// Constructor that initilizes the user by default. 
        /// </summary>
        public UserRepository()
        {
            DbHelper = new SqliteDbHelper(); 
            DbHelper.CreateTables(); 
        }
        #endregion  // Constructors

        #region Methods
        /// <summary>
        /// Creates the user if does not exist in the DB (SQL requests are used)
        /// </summary>
        /// <param name="fullname">String value of fullname of the user</param>
        /// <param name="country">String value of country of the user</param>
        /// <param name="city">String value of city of the user</param>
        /// <param name="password">String value of password of the user</param>
        public void CreateUser(string fullname, string country, string city, 
            string password)
        {
            try
            {
                EncryptPassword(ref password); 
            }
            catch (System.Exception e)
            {
                throw e;
            }

            // Requests for city information. 
            string insertCity = $@"INSERT INTO Cities (CityName) 
                SELECT ('{city}')
                WHERE (SELECT COUNT(1) FROM Cities WHERE CityName = '{city}') = 0;"; 

            // Requests for country information. 
            string insertCounty = $@"INSERT INTO Countries (CountryName, CityIdFK) 
                VALUES (
                    '{country}', 
                    (SELECT CityId FROM Cities WHERE CityName = '{city}')
                );";  
            string checkCountry = $@"SELECT COUNT (1) FROM Countries 
                WHERE CountryName = '{country}';"; 

            // Requests for user information. 
            string insertUser = $@"INSERT INTO Users (Fullname, CountryIdFK, Password) 
                VALUES (
                    '{fullname}', 
                    (SELECT CountryId FROM Countries WHERE CountryName = '{country}'), 
                    '{password}'
                );";  
            string checkUser = $@"SELECT COUNT (1) FROM Users 
                WHERE Fullname = '{fullname}' AND Password = '{password}';"; 
            
            try
            {
                DbHelper.Insert(insertCity); 
                DbHelper.Insert(insertCounty, checkCountry); 
                DbHelper.Insert(insertUser, checkUser); 
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Allows to get current active user using SQL request. 
        /// </summary>
        /// <param name="fullname">String value of fullname of the user</param>
        /// <param name="password">String value of password of the user</param>
        /// <returns>Instance of User class</returns>
        public bool DoesExist(string fullname, string password)
        {
            // Get if input strings are correct. 
            bool isFullnameCorrect = (fullname != null && fullname != string.Empty); 
            bool isPasswordCorrect = (password != null && password != string.Empty); 
            if (!isFullnameCorrect || !isPasswordCorrect)
            {
                throw new System.Exception("Unable to athenticate user in the repository (string cannot be empty or null)."); 
            }

            try
            {
                EncryptPassword(ref password); 
            }
            catch (System.Exception e)
            {
                throw e; 
            }

            string checkUser = $@"SELECT COUNT (1) FROM Users 
                WHERE Fullname = '{fullname}' AND Password = '{password}';"; 
            
            bool exists = false; 
            try
            {
                exists = DbHelper.DoesExist(checkUser); 
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return exists; 
        }

        /// <summary>
        /// Assigns an instance of the user
        /// </summary>
        /// <param name="fullname">String value of the user's fullname</param>
        public void AuthenticateUser(string fullname, string password)
        {
            // Get if input strings are correct. 
            bool isFullnameCorrect = (fullname != null && fullname != string.Empty); 
            bool isPasswordCorrect = (password != null && password != string.Empty); 
            if (!isFullnameCorrect || !isPasswordCorrect)
            {
                throw new System.Exception("Unable to athenticate user in the repository (string cannot be empty or null)."); 
            }

            // Encrypt password and get if the user exists in the DB. 
            try
            {
                if ( !DoesExist(fullname, password) )
                {
                    throw new System.Exception($"Unable to athenticate user in the repository (user {fullname} does not exist in the DB)."); 
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }

            // Get data about the user from DB. 
            string country = string.Empty; 
            string city = string.Empty; 
            EncryptPassword(ref password); 
            DbHelper.GetInfoAboutUser(fullname, out country, out city, password); 

            // Create an instance of the user. 
            UserObj = new User()
            {
                Fullname = fullname, Country = country, City = city 
            }; 
        }

        /// <summary>
        /// Sets an instance of the user equal to null. 
        /// </summary>
        public void LogOutUser()
        {
            UserObj = null; 
        }

        /// <summary>
        /// Allows to get current active user. 
        /// </summary>
        /// <returns>Instance of User class</returns>
        public User GetUser()
        {
            return UserObj; 
        }

        /// <summary>
        /// Allows to encrypt password 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private void EncryptPassword(ref string password)
        {
            SubstitutionCipher cipher = new SubstitutionCipher(); 
            try
            {
                password = cipher.Monoalphabetic(password); 
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
        #endregion  // Methods
    }
}