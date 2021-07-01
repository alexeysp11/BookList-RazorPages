using Microsoft.Data.Sqlite; 

namespace BookList.Services
{
    /// <summary>
    /// Allows to interact with the database. 
    /// </summary>
    public class SqliteDbHelper
    {
        #region Configuration settings
        /// <summary>
        /// Path to the database 
        /// </summary>
        private const string AbsolutePathToDb = "C:\\Users\\User\\Desktop\\projects\\AspNetCore\\BookList\\Databases\\DB.sqlite3"; 
        #endregion  // Configuration settings
        
        #region Create methods
        /// <summary>
        /// Method for creating tables in this application 
        /// </summary>
        public void CreateTables()
        {
            string script = System.IO.File.ReadAllText("C:\\Users\\User\\Desktop\\projects\\AspNetCore\\BookList\\BookList.Services\\Source\\CreateTables.sql");

            var connectionStringBuilder = new SqliteConnectionStringBuilder(); 
            connectionStringBuilder.DataSource = AbsolutePathToDb;
            connectionStringBuilder.Mode = SqliteOpenMode.ReadWriteCreate;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                try
                {
                    connection.Open(); 
                    if (System.IO.File.Exists(connectionStringBuilder.DataSource))
                    {
                        var tableCmd = connection.CreateCommand(); 
                        tableCmd.CommandText = script; 
                        tableCmd.ExecuteNonQuery(); 
                    }
                }
                catch (System.Exception e)
                {
                    throw e; 
                }
            }
        }
        #endregion  // Create methods

        #region Insert methods 
        /// <summary>
        /// Allows to insert data using insert request 
        /// </summary>
        /// <param name="request">String representation of a request</param>
        public void Insert(string request)
        {
            if (request == string.Empty)
            {
                throw new System.Exception("Request cannot be empty"); 
            }

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = AbsolutePathToDb;
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var insertCmd = connection.CreateCommand(); 
                        insertCmd.CommandText = request;            // SQL command. 
                        insertCmd.ExecuteNonQuery();                // Execute SQL command. 
                        transaction.Commit();                       // Commit changes. 
                    }
                }
                catch (System.Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Allows to insert data using with foreign key 
        /// </summary>
        /// <param name="insertRequest">String representation of an insert request</param>
        /// <param name="checkRequest">String representation of a check request</param>
        public void Insert(string insertRequest, string checkRequest)
        {
            if (insertRequest == string.Empty || checkRequest == string.Empty)
            {
                throw new System.Exception("Request cannot be empty"); 
            }

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = AbsolutePathToDb;
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                try
                {
                    connection.Open();

                    bool exists = false; 

                    var selectCmd = connection.CreateCommand();
                    selectCmd.CommandText = checkRequest; 
                    using (var reader = selectCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) != 0)
                            {
                                exists = true; 
                            }
                        }
                    }

                    if (!exists)
                    {
                        using (var transaction = connection.BeginTransaction())
                        {
                            var insertCmd = connection.CreateCommand(); 
                            insertCmd.CommandText = insertRequest; 
                            insertCmd.ExecuteNonQuery(); 
                            transaction.Commit(); 
                        }
                    }
                }
                catch (System.Exception e)
                {
                    throw e;
                }
            }
        }
        #endregion  // Insert methods 

        #region Update methods 
        /// <summary>
        /// Allows to update a table 
        /// </summary>
        /// <param name="request">String representation of an insert request</param>
        public void Update(string request)
        {
            if (request == string.Empty)
            {
                throw new System.Exception("Request cannot be empty. "); 
            }

            try
            {
                Insert(request); 
            }
            catch (System.Exception e)
            {
                throw e; 
            }
        }
        #endregion  // Update methods 

        #region Read methods
        /// <summary>
        /// Checks if an element exists in the database. 
        /// </summary>
        /// <param name="readRequest">Request for reading data</param>
        /// <returns></returns>
        public bool DoesExist(string readRequest)
        {
            if (readRequest == string.Empty)
            {
                throw new System.Exception("Request cannot be empty"); 
            }

            bool exists = false; 
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = AbsolutePathToDb;
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                try
                {
                    connection.Open();

                    var selectCmd = connection.CreateCommand();
                    selectCmd.CommandText = readRequest; 
                    using (var reader = selectCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) != 0)
                            {
                                exists = true; 
                            }
                        }
                    }
                }
                catch (System.Exception e)
                {
                    throw e; 
                }
            }
            return exists; 
        }
        #endregion  // Read methods
    }
}
