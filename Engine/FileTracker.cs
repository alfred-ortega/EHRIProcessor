using System;
namespace EHRIProcessor.Engine
{
    class FileTracker
    {
        private string fileID;
        private bool fileExists = false;
        public bool FileExists {get {return fileExists;}}

        public FileTracker(string fileName)
        {
            checkIfFileExists(fileName);
            if(!fileExists)
                addFile(fileName);
        }

        void checkIfFileExists(string fileName)
        {
            string sql = "Select count(*) T from olu.EHRI_InsertTrainingFileInfo where filename = \'" + fileName + "\'";
            fileExists = int.Parse(Database.ExecuteScalar(sql).ToString()) == 1;
        }

        private void addFile(string fileName)
        {
            fileID = Guid.NewGuid().ToString();


        }


    }
}

//trainingfileinfo