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

        public EhriFileWriter()
        {

        }

        public void Write()
        {
            OluContext oluContext = new OluContext();
            List<EhriTraining> records = (from t in oluContext.EhriTraining
                                         where t.ProcessStatus == "R" && t.LrnInterfaceOutId == null
                                         select t).ToList();

            string fileName = getFileName();

            XMLWriter xmlWriter = new XMLWriter();
            xmlWriter.WriteEhriFile(records,fileName);
        }

        private string getFileName()
        {
            return Config.Settings.TransferDirectory +  "somefilenamehere.xml";
        }

    }//end class
}//end namespace