using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace EHRIProcessor.Model
{
    public sealed class TrainingRecordValues
    {
        public string[] DeliveryType;
        public string[] CreditType;
        public string[] SourceType;
        public string[] DesignationType;
        public string[] TrainingType ;        
        public string[] TrainingSubType;  
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
            string json = File.ReadAllText(Directory.GetCurrentDirectory() + "\\sf182values.json");
            Dictionary<string, string> valueList = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            DeliveryType = valueList["DeliveryType"].Split(",");
            CreditType = valueList["CreditType"].Split(",");
            SourceType = valueList["SourceType"].Split(",");
            DesignationType = valueList["DesignationType"].Split(",");
            TrainingType = valueList["TrainingType"].Split(",");
            TrainingSubType = valueList["TrainingSubType"].Split(",");
        }

      

    }//end class
}//end namespace