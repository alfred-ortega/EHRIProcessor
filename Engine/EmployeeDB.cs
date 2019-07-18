using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EHRIProcessor.Model;
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
        public List<EhriEmployee> Employees;   
        public EmployeeDB()
        {
        }
        public void UpdateEmployeeData()
        {
            string[] commands ={"EHRI_InsertEmployees","EHRI_UpdateEmployees","EHRI_DeleteEmployees"};
            foreach(string command in commands)
            {
                Logger.Log.Record("Begin running " + command);
                Database.RunCommand(command);
                Logger.Log.Record("Completed running of " + command);
            }
            loadEmployees();
        }

        void loadEmployees()
        {
            using(OluContext db = new OluContext())
            {

                Employees = db.EhriEmployee.ToList();
                int i = Employees.Count();
                Logger.Log.Record(i + " employees loaded");
            }
        }
        public EhriEmployee SelectEmployee(string employeeID)
        {
            EhriEmployee emp = new EhriEmployee();
            try
            {
                emp = Employees.Where(s => s.Emplid == employeeID).SingleOrDefault();
                if(emp.Emplid == string.Empty)
                {
                   Logger.Log.Record(LogType.Error, "Employee " + employeeID + " not found.");
                }
                
            }
            catch(NullReferenceException)
            {
                Logger.Log.Record(LogType.Error, "Employee " + employeeID + " not found.");
            }
            catch(Exception x)
            {
                Logger.Log.Record(LogType.Error, x.ToString());
            }
            return emp;
        }

        
    }//end class
}//end namespace