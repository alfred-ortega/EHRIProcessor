using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace EHRIProcessor.Engine
{
    /// <summary>
    /// Database Wrapper for executing commands against the MySql database.
    /// </summary>
    class Database
    {


       public static object ExecuteScalar(MySqlCommand command)
        {
            object retval = null;
            using(MySqlConnection connection = new MySqlConnection(Config.Settings.OluDB))
            {
                command.Connection = connection;
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                retval = command.ExecuteScalar();
                connection.Close();
            }//end using connection
            return retval;
        }


       public static object ExecuteScalar(string sqlText)
        {
            object retval = null;
            using(MySqlConnection connection = new MySqlConnection(Config.Settings.OluDB))
            {
                using(MySqlCommand command = new MySqlCommand(sqlText,connection))
                {
                    command.Connection = connection;
                    connection.Open();
                    command.CommandType = CommandType.Text;
                    retval = command.ExecuteScalar();
                    connection.Close();
                }
            }//end using connection
            return retval;
        }

       public static void RunCommand(MySqlCommand command)
        {
            using(MySqlConnection connection = new MySqlConnection(Config.Settings.OluDB))
            {
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }//end using connection
        }

       public static void RunCommand(string storedProcedureName)
        {
            using(MySqlConnection connection = new MySqlConnection(Config.Settings.OluDB))
            {
                using(MySqlCommand command = new MySqlCommand(storedProcedureName,connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    connection.Close();
                }//end using command
            }//end using connection
        }
    }//end class
}//end namespace