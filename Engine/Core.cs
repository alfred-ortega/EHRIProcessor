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
                    executeEHRIProcess();           
                }
            }
            archiveTrainingFiles();
            Console.WriteLine("Done");
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

        void archiveTrainingFiles()
        {
            foreach(string file in trainingFiles)
            {
                string archiveDirectory = Config.Settings.ArchiveDirectory;
                string BaseDirectory = Config.Settings.BaseDirectory;
                string newFileName = file.Replace(BaseDirectory,archiveDirectory);
                string dateStamp = "_" + DateTime.Now.ToString("yyyMMdd") + ".csv";
                newFileName = newFileName.Replace(".csv",dateStamp);
                if(File.Exists(newFileName))
                    File.Delete(newFileName);

                File.Move(file,newFileName);
            }
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
                    dataMerger.MergeEmployeeTrainingData(trainingRecordLoader.OLURecords,employeeDb.Employees,fileTracker.FileID);
                    trainingRecordLoader.OLURecords = dataMerger.RemoveDuplicateRecords(trainingRecordLoader.OLURecords);
                    dataMerger.Save(trainingRecordLoader.OLURecords);
                    fileTracker.UpdateCount(trainingRecordLoader.OLURecords.Count);
                }
            }
        }


        private void executeEHRIProcess()
        {
            EhriFileWriter writer = new EhriFileWriter();
            writer.Write();

            MainFrameCDPWriter mfWriter = new MainFrameCDPWriter();
            mfWriter.Write();
        }
#endregion


    }//end class
}//end namespace