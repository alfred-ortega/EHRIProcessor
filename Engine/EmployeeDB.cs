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
                Console.WriteLine(DateTime.Now.ToLongTimeString() +  " begin running " + command);
                Database.RunCommand(command);
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " completed running of " + command);
            }
            loadEmployees();
        }

        void loadEmployees()
        {
            using(OluContext db = new OluContext())
            {

                Employees = db.EhriEmployee.ToList();
                int i = Employees.Count();
                Console.WriteLine(i + " employees loaded");
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
                   throw new Exception("Employee " + employeeID + " not found.");
                }
                
            }
            catch(Exception x)
            {
                throw x;
            }
            return emp;
        }

        
    }//end class
}//end namespace