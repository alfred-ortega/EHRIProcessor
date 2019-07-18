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
       public static void RunCommand(string storedProcedureName)
        {
            using(MySqlConnection connection = new MySqlConnection(Config.Settings.OluDB))
            {
                using(MySqlCommand command = new MySqlCommand(storedProcedureName,connection))
                {
                    try
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception x)
                    {
                        Logger.Log.Record(LogType.Error, x.ToString());
                        throw x;
                    }
                }//end using command
            }//end using connection
        }
    }//end class
}//end namespace