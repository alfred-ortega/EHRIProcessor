using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace EHRIProcessor.Engine
{
    /// <summary>
    /// The EmployeeDBUpdater fires three commands against the database.
    /// 1. Inserts new employees into the employee history table
    /// 2. Updates existing employee records in the employee history table
    /// 3. Deletes employees from the employee history table that have been inactive for 5 years.
    /// </summary>
    class EmployeeDB
    {
        public EmployeeDB()
        {
        }
        public void UpdateEmployeeData()
        {
            string[] commands ={"EHRI_InsertEmployees","EHRI_UpdateEmployees","EHRI_DeleteEmployees"};
            foreach(string command in commands)
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString() +  " begin running " + command);
                Database.RunCommand(command);
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " completed running of " + command);
            }
        }


        
    }//end class
}//end namespace