using System;
using System.IO;
using System.Collections.Generic;
using EHRIProcessor.Model;

namespace EHRIProcessor.Engine
{
    class TrainingRecordLoader
    {
        public List<EhriTraining> OLURecords;
     

        public TrainingRecordLoader()
        {
            OLURecords = new List<EhriTraining>();

        }

        public void Load(string trainingFile)
        {
            try 
            {
                string[] trainingEntries = File.ReadAllLines(trainingFile);
                string[] cleanedLines = removeDoubleQuotes(trainingEntries);
                loadOluRecords(cleanedLines);

            }
            catch(Exception x)
            {
                Console.WriteLine(x.ToString());
            }
        }

        string[]  removeDoubleQuotes(string[] lines)
        {
            string[] newLines = new string[lines.Length];
            int i = 0;
            foreach (string line in lines)
            {
                string retval = string.Empty;
                //remove double quote at start and end of each line.
                //we don't replace because we need to test if there is double quotes 
                //in the line else where to handle.
                retval = line.Substring(1);//chop off first double quote
                retval = retval.Substring(0, retval.Length - 1);//chop of ending double quote
                int dqTest = retval.IndexOf("\"");
                if (dqTest > 0)
                {
                    retval = retval.Replace("\"", string.Empty); // \"\"WIFM\"\" 
                }
                newLines[i] = retval;
                i++;
            }
            return newLines;
        }

        void loadOluRecords(string[] trainingEntries)
        {
            int recordLine = 0;
            foreach (string trainingEntry in trainingEntries)
            {
                try
                {
                    recordLine++;
                    EhriTraining record = new EhriTraining();
                    string[] data = trainingEntry.Split("~");

                    record.CreatedDate = DateTime.Now;
                    record.EmployeeFirstName = data[0];
                    record.EmployeeLastName = data[1];
                    record.EmailAddress = data[2];
                    record.TrainingPurpose = data[3];
                    record.TrainingSource = data[4];
                    record.TrainingType = checkForNull(data[5],"Basic Training Area");
                    record.CourseTitle = data[6];
                    record.CourseStartDate = data[7];
                    record.CourseCompletionDate = data[8];
                    record.PersonId = data[9];
                    record.TrainingDeliveryType = checkForNull(data[10],"Technology Based");
                    record.CreditDesignation = checkForNull(data[11],"N/A");
                    record.TrainingSubType = data[12]; //  checkForNull(data[12],"Agency Required Training");
                    record.DutyHours = decimal.Parse(checkForNull(data[13],"1"));
                    record.NonDutyHours =decimal.Parse(checkForNull(data[14],"0"));
                    record.TrainingCredit = decimal.Parse(checkForNull(data[15],"1"));
                    record.VendorName = checkForNull(data[16],"GSA OLU");
                    record.TrainingLocation = checkForNull(data[17],"Online");
                    record.RepaymentAgreementReqd = checkForNull(data[18],"N");
                    record.ContServiceAgreementSigned =checkForNull(data[19],"NA") ;
                    record.TutionAndFees = decimal.Parse(checkForNull(data[20],"0.0"));
                    record.TravelCosts = decimal.Parse(checkForNull(data[21],"0.0"));
                    record.NongovtContrCost = decimal.Parse(checkForNull(data[22],"0.0"));
                    record.MaterialCost = decimal.Parse(checkForNull(data[23],"0.0"));
                    record.PerdiemCost = decimal.Parse(checkForNull(data[24],"0.0"));
                    record.CreditType = checkForNull(data[25],"");
                    record.AccreditationIndicator = checkForNull(data[26],"N");
                    record.PriorKnowledgeLevel = data[27];
                    record.ImpactOnPerformance = data[28];
                    record.RecommendTrngToOthers = data[29];
                    record.TrainingPartOfIdp = data[30];
                    record.PriorSupvApprovalReceived = data[31];
                    record.TypeOfPayment = data[32];
                    record.TrainingTravelIndicator = data[33];
                    record.CourseType = data[34];
                    record.CourseId = data[35];
                    OLURecords.Add(record);
                }
                catch(Exception x)
                {
                    Console.WriteLine("Error with record " + recordLine.ToString() + ":" + x.Message);
                }

            }
        }

        private string checkForNull(string testvalue, string defaultValue)
        {
            string retval = defaultValue;
            if(testvalue.Trim().Length>0)
                retval = testvalue;
            return retval;
        }

    }//end class
}//end namespace