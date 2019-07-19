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

        public void MergeEmployeeTrainingData(List<EhriTraining> trainingRecords, List<EhriEmployee> employees, string fileId)
        {
            Employees = employees;
            TrainingRecords = trainingRecords;            
            foreach(EhriTraining trainingrecord in trainingRecords)
            {
                try
                {
                    EhriEmployee student = selectEmployee(trainingrecord.PersonId);
                    if(student.Emplid != string.Empty)
                    {
                        trainingrecord.TrainingFileId = fileId;
                        trainingrecord.EmployeeFirstName = student.FirstName;
                        trainingrecord.EmployeeMiddleName = student.MiddleName;
                        trainingrecord.EmployeeLastName = student.LastName;
                        trainingrecord.Ssn = student.Ssn;
                        trainingrecord.BirthDate = student.Birthdate;
                        trainingrecord.AgencySubElement = student.AgencySubElement;
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
            }//end foreach
        }        

        public List<EhriTraining> RemoveDuplicateRecords(List<EhriTraining> records)
        {
            List<EhriTraining> returnRecords = new List<EhriTraining>();
            int loopCount = 0;
            int errorCount = 0;
            using(OluContext db = new OluContext())
            {
                foreach(EhriTraining record in records)
                {

                    var count = (from t in db.EhriTraining
                                 where t.Ssn == record.Ssn 
                                 && t.CourseId == record.CourseId
                                 && t.CourseCompletionDate == record.CourseCompletionDate
                                 select t).Count();
                    

                    if(count > 0)
                    {
                        errorCount++;
                    }
                    else
                    {
                        returnRecords.Add(record);
                    }
                    

                    loopCount++;
                    if(loopCount % 100 == 0)                       
                    {
                        Console.WriteLine("Verified " + loopCount + " records");
                    } 
                }
            }
            Logger.Log.Record("Completed removing duplicate records " + errorCount + " duplicates found");
            return returnRecords;
        }

        public void Save(List<EhriTraining> records)
        {
            Logger.Log.Record("Saving " + records.Count + " records");
            using(OluContext context = new OluContext())
            {
                foreach(EhriTraining record in records)
                {
                    context.EhriTraining.Add(record);
                }
                context.SaveChangesAsync();
            }
            Logger.Log.Record("Saving completed");
        }




        private EhriEmployee selectEmployee(string employeeID)
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