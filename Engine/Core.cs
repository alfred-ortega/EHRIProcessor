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
            Logger.Log.Record("Beging Core.Execute");

            bool filesExist = checkForTrainingFile();

            //if no files exist then just quit... else do the work
            if(filesExist)
            {
                employeeDb = new EmployeeDB();
                employeeDb.UpdateEmployeeData();
                loadTrainingFiles();
                if(doEHRI)
                {
                    Logger.Log.Record("Beginning EHRI TransferFile Creation Process.");
                    executeEHRIProcess();           
                    Logger.Log.Record("Completed EHRI TranferFile Creation Process.");
                }
            }
            else
            {
                Logger.Log.Record("No files to process found.");
            }
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
                    Logger.Log.Record("Monthly Training File Found: " + trainingFile);
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
                {
                    File.Delete(newFileName);
                    Logger.Log.Record("Previous version of archive file " + newFileName + " deleted.");

                }
                File.Move(file,newFileName);
                Logger.Log.Record("Archive file " + newFileName + " created.");
            }
        }



#region "Training Records"

        private void loadTrainingFiles()
        {
            Logger.Log.Record("Begin Load Training File Process");
            Logger.Log.Record("Training Files to load: " + trainingFiles.Length.ToString());
            foreach(string trainingFile in trainingFiles)
            {
                Logger.Log.Record("Checking if '" + trainingFile + "' previously processed");
                FileTracker fileTracker = new FileTracker(trainingFile); 
                if(!fileTracker.FileExists)
                {
                    TrainingRecordLoader trainingRecordLoader = new TrainingRecordLoader();
                    Logger.Log.Record("Loading " + trainingFile);
                    trainingRecordLoader.Load(trainingFile);
                    DataMerger dataMerger = new DataMerger();
                    Logger.Log.Record("Merging training records with employee records");
                    dataMerger.MergeEmployeeTrainingData(trainingRecordLoader.OLURecords,employeeDb.Employees,fileTracker.FileID);
                    Logger.Log.Record("Removing duplicate training records");
                    trainingRecordLoader.OLURecords = dataMerger.RemoveDuplicateRecords(trainingRecordLoader.OLURecords);
                    Logger.Log.Record("Saving merged records");
                    dataMerger.Save(trainingRecordLoader.OLURecords);
                    Logger.Log.Record("Updating saved record count on file tracker");
                    fileTracker.UpdateCount(trainingRecordLoader.OLURecords.Count);
                }
                else
                {
                    Logger.Log.Record("File '" + trainingFile + "' already processed" );
                }
            }
            Logger.Log.Record("Beginning File Archive Process.");
            archiveTrainingFiles();
            Logger.Log.Record("Completed File Archive Process.");            
            Logger.Log.Record("Completed Load Training File process");
        }


        private void executeEHRIProcess()
        {
            Logger.Log.Record("Begin writing EHRI File");
            EhriFileWriter writer = new EhriFileWriter();
            writer.Write();
            Logger.Log.Record("Completed writing of EHRI File");
            Logger.Log.Record("Begin writing of MainFrame Copy File");
            MainFrameCDPWriter mfWriter = new MainFrameCDPWriter();
            mfWriter.Write();
            Logger.Log.Record("Completed writing of MainFrame Copy File");
        }
#endregion


    }//end class
}//end namespace