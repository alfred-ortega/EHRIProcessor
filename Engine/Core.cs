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
        IQueryable<EhriEmployee> Students;        
        string[] trainingFiles;
        bool doEHRI;
        public Core()
        {

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
            using(OluContext db = new OluContext())
            {

                Students = db.EhriEmployee;
                int i = Students.Count();
                Console.WriteLine(i + "records");
            }
        }

        EhriEmployee selectStudent(string employeeID)
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
                    int i = mergeEmployeeTrainingData(trainingRecordLoader.OLURecords,fileTracker.FileID);
                    fileTracker.UpdateCount(i);
                }
            }
        }
        int mergeEmployeeTrainingData(List<EhriTraining> trainingRecords, string fileId)
        {
            int i = 0;
            OluContext db = new OluContext();

            foreach(EhriTraining trainingrecord in trainingRecords)
            {
                EhriEmployee student = selectStudent(trainingrecord.PersonId);
                trainingrecord.TrainingFileInfoId = fileId;
                trainingrecord.EmployeeFirstName = student.FirstName;
                trainingrecord.EmployeeMiddleName = student.MiddleName;
                trainingrecord.EmployeeLastName = student.LastName;
                trainingrecord.Ssn = student.Ssn;
                trainingrecord.BirthDate = student.Birthdate;
                trainingrecord.CheckIfValid();
                db.EhriTraining.Add(trainingrecord);
                i++;
            }

            db.SaveChanges();
            Console.WriteLine(i + " records added");
            return i;
        }

        private void executeEHRIProcess(List<Model.EhriTraining> trainingRecords)
        {
            //throw new NotImplementedException();
        }
#endregion


    }//end class
}//end namespace