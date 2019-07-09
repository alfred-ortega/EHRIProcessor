using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using System;
using EHRIProcessor.Model;

namespace EHRIProcessor.Engine
{
    class Core
    {
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

#region "Employee Update"
        void updateEmployees()
        {
            EmployeeDBUpdater db = new  EmployeeDBUpdater();
            db.UpdateEmployeeData();
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
                    fileTracker.UpdateCount(i);
                }
            }
        }

        private void executeEHRIProcess(List<OLURecord> trainingRecords)
        {
            //throw new NotImplementedException();
        }
#endregion


    }//end class
}//end namespace