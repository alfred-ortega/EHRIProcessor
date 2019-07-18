using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using EHRIProcessor.Model;



namespace EHRIProcessor.Engine
{
    class EhriFileWriter
    {
        EhriTransmissionfile transmissionFile;
        string fileName;
        XMLWriter xmlWriter;

        public EhriFileWriter()
        {
            transmissionFile = new EhriTransmissionfile();
            fileName = getFileName();
            xmlWriter = new XMLWriter();

        }

        public void Write()
        {
            Logger.Log.Record("Preparing records for EHRI Transmission");
            int recordsToSendCount = 0;
            using(OluContext oluContext = new OluContext())
            {
                List<EhriTraining> records = (from t in oluContext.EhriTraining
                                            where t.ProcessStatus == "R"
                                            select t).ToList();
                recordsToSendCount = records.Count;
                xmlWriter.WriteEhriFile(records,fileName);
                foreach(EhriTraining record in records)
                {
                    record.TransmissionFileId = transmissionFile.TransmissionFileId;
                    record.LastUpdatedDate = DateTime.Now;
                    record.ProcessStatus = "S";
                    oluContext.EhriTraining.Update(record);
                }
                oluContext.SaveChanges();
            }
            saveTransmissionInfo(recordsToSendCount);
            Logger.Log.Record("EHRI Transmission records updated");
        }



        void saveTransmissionInfo(int recordCount)
        {
            using(OluContext oluContext = new OluContext())
            {
                transmissionFile.RecordCount = recordCount;
                transmissionFile.SentToOpmdate = DateTime.Now;
                oluContext.EhriTransmissionfile.Add(transmissionFile);
                oluContext.SaveChanges();
            }

        }

        private string getFileName()
        {
            return Config.Settings.TransferDirectory + transmissionFile.FileName;
        }

    }//end class
}//end namespace