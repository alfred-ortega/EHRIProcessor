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
        EmployeeDB employeeDb;
     
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
                employeeDb = new EmployeeDB();
                employeeDb.UpdateEmployeeData();
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
                }
            }
            return trainingFiles.Length > 0;
        }



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
                    DataMerger dataMerger = new DataMerger();
                    int i = dataMerger.MergeEmployeeTrainingData(trainingRecordLoader.OLURecords,employeeDb.Employees,fileTracker.FileID);
                    fileTracker.UpdateCount(i);
                }
            }
        }


        private void executeEHRIProcess(List<Model.EhriTraining> trainingRecords)
        {
            XMLWriter x = new XMLWriter();
        }
#endregion


    }//end class
}//end namespace