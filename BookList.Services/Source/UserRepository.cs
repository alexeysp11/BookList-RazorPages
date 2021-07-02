using System.Collections.Generic; 
using BookList.Models; 

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
            // Insert city information. 
            string insertCity = $@"INSERT INTO Cities (CityName) 
                SELECT ('{city}')
                WHERE (SELECT COUNT(1) FROM Cities WHERE CityName = '{city}') = 0;"; 

            // Insert country information. 
            string insertCounty = $@"INSERT INTO Countries (CountryName, CityIdFK) 
                VALUES (
                    '{country}', 
                    (SELECT CityId FROM Cities WHERE CityName = '{city}')
                );";  
            string checkCountry = $@"SELECT COUNT (1) FROM Countries 
                WHERE CountryName = '{country}';"; 

            // Insert user information. 
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
        public void AuthenticateUser(string fullname)
        {
            UserObj = new User()
            {
                Fullname = fullname
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
        #endregion  // Methods
    }
}