using System;
using System.Collections.Generic;
using System.IO;
using EHRIProcessor.Engine;
using Newtonsoft.Json;

namespace EHRIProcessor.Model
{
    public sealed class TrainingRecordValues
    {
        string json; 
        Dictionary<string,Dictionary<string,string>> ehriDictionary;
        public Dictionary<string,string> DeliveryType;
        public Dictionary<string,string> CreditType;
        public Dictionary<string,string> SourceType;
        public Dictionary<string,string> DesignationType;
        public Dictionary<string,string> TrainingType ;        
        public Dictionary<string,string> TrainingSubType;  
        public Dictionary<string,string> PurposeType;
        private static readonly TrainingRecordValues value = new TrainingRecordValues();

        public static TrainingRecordValues Value{
            get{
                return value;
            }
        }

        static TrainingRecordValues()
        {}

        private TrainingRecordValues()
        {
            loadPrimaryDictionary();
            loadSubDictionaries();
        }

        private void loadPrimaryDictionary()
        {
            json = File.ReadAllText(Config.Settings.ConfigDirectory + "\\ehricodes.json");
            ehriDictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string,string>>>(json);
        }

        private void loadSubDictionaries()
        {
            DeliveryType = loadValues("DeliveryType");
            CreditType = loadValues("CreditType");
            SourceType = loadValues("SourceType");
            DesignationType = loadValues("DesignationType");
            TrainingType = loadValues("TrainingType");
            TrainingSubType = loadValues("TrainingSubType");  
            PurposeType = loadValues("PurposeType");          
        }


        private Dictionary<string,string> loadValues(string codesToFind)
        {
            Dictionary<string,string> dataSet = new Dictionary<string,string>();
            foreach(KeyValuePair<string,Dictionary<string,string>> entry in ehriDictionary)
            {
                if(entry.Key == codesToFind)
                    dataSet = entry.Value;
            }
            Console.WriteLine("Loaded " + dataSet.Count + " records for field " + codesToFind );

            return dataSet;
        }

      

    }//end class
}//end namespace