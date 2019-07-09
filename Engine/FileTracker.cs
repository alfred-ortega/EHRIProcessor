using System;
using System.Linq;
using EHRIProcessor.Model;
namespace EHRIProcessor.Engine
{
    class FileTracker
    {
        public string FileID;
        private bool fileExists = false;
        public bool FileExists {get {return fileExists;}}

        public FileTracker(string fileName)
        {
            checkIfFileExists(fileName);
            if(!fileExists)
                addFile(fileName);
        }

        public void UpdateCount(string fileID, int processedCount)
        {
            using (oluContext db = new oluContext())
            {
                var trainingRecord = db.TrainingFileInfo
                                     .Single(t => t.TrainingFileInfoId == fileID);

                trainingRecord.SavedRecordCount = processedCount;

                db.SaveChanges();

            }
        }

        void checkIfFileExists(string fileName)
        {
            using (oluContext db = new oluContext())
            {
                var trainingRecord = (from t in db.TrainingFileInfo
                                      where t.FileName == fileName
                                      select t)
                                     .Count();

                fileExists = trainingRecord == 1;
            }
        }

        private void addFile(string fileName)
        {
            FileID = Guid.NewGuid().ToString();
            TrainingFileInfo tf = new TrainingFileInfo();
            tf.TrainingFileInfoId = FileID;
            tf.FileName = fileName;
            tf.Loaded = DateTime.Now;
            tf.FileRecordCount = getFileRecordCount(fileName);
            using(oluContext db = new oluContext())
            {
                db.TrainingFileInfo.Add(tf);
                db.SaveChanges();
            }




        }

        private int getFileRecordCount(string fileName)
        {
            int i = 0;
            try
            {
                return System.IO.File.ReadAllLines(fileName).Length;
            }
            catch(Exception)
            {
                i = -1;
            }
            return i;
        }


    }
}
