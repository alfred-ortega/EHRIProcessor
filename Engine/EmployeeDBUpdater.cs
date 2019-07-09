using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace EHRIProcessor.Engine
{
    class EmployeeDBUpdater
    {
        public EmployeeDBUpdater()
        {
        }
        public void UpdateEmployeeData()
        {
            string[] commands ={"EHRI_InsertEmployees","EHRI_UpdateEmployees","EHRI_DeleteEmployees"};
            foreach(string command in commands)
            {
                Database.RunCommand(command);
            }
        }

        
    }//end class
}//end namespace