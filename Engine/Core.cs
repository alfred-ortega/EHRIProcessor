using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using EHRIProcessor.Model;

namespace EHRIProcessor.Engine
{
    class Core
    {
        List<EhriStudHist> Students;        
        string[] trainingFiles;
        bool doEHRI;
        public Core()
        {
            Students = new List<EhriStudHist>();
        }


        public void Execute()
        {
            bool filesExist = checkForTrainingFile();

            //if no files exist then just quit... else do the work
            if(filesExist)
            {
                updateEmployees();
                loadEmployees();
                loadTrainingFiles();
                if(doEHRI)
                {
                    //executeEHRIProcess(trainingRecordLoader.OLURecords);                
                }

            }
        }
        private bool checkForTrainingFile()
        {
            trainingFiles = Directory.GetFiles(Config.Settings.BaseDirectory);
            foreach(string trainingFile in trainingFiles)
            {
                bool t = trainingFile.Contains("monthly");
                if(t)
                {
                    doEHRI = true;
                    break;
                }
            }

            return trainingFiles.Length > 0;
        }

#region "Employees"
        void updateEmployees()
        {
            EmployeeDB db = new  EmployeeDB();
            db.UpdateEmployeeData();
        }

        void loadEmployees()
        {
            using(oluContext db = new oluContext())
            {
                Students = db.EhriStudHist.ToList();
            }
        }

        EhriStudHist selectStudent(string employeeID)
        {
            return Students.Where(s => s.Emplid == employeeID).Single();
        }


#endregion

#region "Training Records"

        private void loadTrainingFiles()
        {
            foreach(string trainingFile in trainingFiles)
            {
                FileTracker fileTracker = new FileTracker(trainingFile); 
                if(!fileTracker.FileExists)
                {
                    TrainingRecordLoader trainingRecordLoader = new TrainingRecordLoader();
                    trainingRecordLoader.Load(trainingFile);
                    int i = 0;
                    mergeEmployeeTrainingData(trainingRecordLoader.OLURecords);
                    fileTracker.UpdateCount(i);
                }
            }
        }
        void mergeEmployeeTrainingData(List<TrainingRecord> trainingRecords)
        {
            int i = 0;
            oluContext db = new oluContext();

            foreach(TrainingRecord trainingrecord in trainingRecords)
            {
                EhriStudHist student = selectStudent(trainingrecord.PersonId);
                trainingrecord.EmployeeFirstName = student.FirstName;
                trainingrecord.EmployeeMiddleName = student.MiddleName;
                trainingrecord.EmployeeLastName = student.LastName;
                trainingrecord.SSN = student.Ssn;
                trainingrecord.Birth_Date = student.Birthdate.ToShortDateString();
                db.EhriTraining.Add(trainingrecord);
                i++;
            }

            db.SaveChanges();
            Console.WriteLine(i + " records added");
        }

        private void executeEHRIProcess(List<TrainingRecord> trainingRecords)
        {
            //throw new NotImplementedException();
        }
#endregion


    }//end class
}//end namespace