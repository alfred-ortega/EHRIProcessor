using System;
using System.Linq;
using EHRIProcessor.Model;
namespace EHRIProcessor.Engine
{
    /// <summary>
    /// The File Tracker object verifies what files have been processed, how many records were in the file and how many
    ///  of those records were saved in the database as valid.  It does this by way of the TrainingFileInfo class.
    /// </summary>
    class FileTracker
    {
        public string FileID;
        private bool fileExists = false;
        public bool FileExists {get {return fileExists;}}
        /// <summary>
        /// The FileTracker constructor takes in the filename that will be parsed and tracked.
        /// </summary>
        /// <param name="fileName">name of the file to track</param>
        public FileTracker(string fileName)
        {
            checkIfFileExists(fileName);
            if(!fileExists)
                addFile(fileName);
        }

        public void UpdateCount(int processedCount)
        {
            using (OluContext db = new OluContext())
            {
                var trainingRecord = (from t in db.EhriTrainingfileinfo
                                     where t.TrainingFileInfoId == FileID
                                     select t).First();


                trainingRecord.SavedRecordCount = processedCount;

                db.SaveChanges();
            }
        }

        void checkIfFileExists(string fileName)
        {
            using (OluContext db = new OluContext())
            {
                var trainingRecord = db.EhriTrainingfileinfo.FirstOrDefault(t => t.FileName == fileName);
                
                if(trainingRecord!=null)
                {
                    FileID = trainingRecord.TrainingFileInfoId;
                    fileExists = true;
                }                     
                else
                {
                    fileExists=false;
                }               
            }
        }

        private void addFile(string fileName)
        {
            FileID = Guid.NewGuid().ToString();
            EhriTrainingfileinfo tf = new EhriTrainingfileinfo();
            tf.TrainingFileInfoId = FileID;
            tf.FileName = fileName;
            tf.Loaded = DateTime.Now;
            tf.FileRecordCount = getFileRecordCount(fileName);
            using(OluContext db = new OluContext())
            {
                db.EhriTrainingfileinfo.Add(tf);
                db.SaveChanges();
            }
        }

        private int getFileRecordCount(string fileName)
        {
            int i = 0;
            try
            {
                i = System.IO.File.ReadAllLines(fileName).Length;
            }
            catch(Exception)
            {
                i = -1;
            }
            return i;
        }


    }
}
