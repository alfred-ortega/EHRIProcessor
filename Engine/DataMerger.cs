using System;
using System.Data;
using MySql.Data.MySqlClient;
using EHRIProcessor.Model;
using System.Collections.Generic;
using System.Linq;

namespace EHRIProcessor.Engine
{
    /// <summary>
    /// Database Wrapper for executing commands against the MySql database.
    /// </summary>
    class DataMerger
    {
        List<EhriEmployee> Employees;
        List<EhriTraining> TrainingRecords;

        public DataMerger()
        {

        }

        public int MergeEmployeeTrainingData(List<EhriTraining> trainingRecords, List<EhriEmployee> employees, string fileId)
        {
            Employees = employees;
            TrainingRecords = trainingRecords;            
            int i = 0;
            OluContext db = new OluContext();

            foreach(EhriTraining trainingrecord in trainingRecords)
            {
                try
                {
                    EhriEmployee student = selectEmployee(trainingrecord.PersonId);
                    if(student.Emplid != string.Empty)
                    {
                        trainingrecord.TrainingFileInfoId = fileId;
                        trainingrecord.EmployeeFirstName = student.FirstName;
                        trainingrecord.EmployeeMiddleName = student.MiddleName;
                        trainingrecord.EmployeeLastName = student.LastName;
                        trainingrecord.Ssn = student.Ssn;
                        trainingrecord.BirthDate = student.Birthdate;
                        trainingrecord.CheckIfValid();
                    }
                }
                catch (System.Exception x)
                {
                    trainingrecord.Ssn = "000000000";
                    trainingrecord.BirthDate = DateTime.Now;
                    trainingrecord.ErrorMessage = x.Message;
                    trainingrecord.ProcessStatus = "X";   
                }
                db.EhriTraining.Add(trainingrecord);
                i++;
            }//end foreach
            Console.WriteLine("Saving " + i + " records.");
            db.SaveChanges();
            Console.WriteLine(i + " records saved");
            return i;
        }        

        EhriEmployee selectEmployee(string employeeID)
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