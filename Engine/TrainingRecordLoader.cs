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
            foreach (string trainingEntry in trainingEntries)
            {
                EhriTraining record = new EhriTraining();
                string[] data = trainingEntry.Split("~");
                for (int i = 0; i < data.Length; i++)
                {

                    record.CreatedDate = DateTime.Now;
                    record.EmployeeFirstName = data[0];
                    record.EmployeeLastName = data[1];
                    // //TODO: NEED TO ACCOUNT FOR MIDDLE NAME
                    record.EmailAddress = data[2];
                    record.TrainingPurpose = data[3];
                    record.TrainingSource = data[4];
                    record.TrainingType = data[5];
                    record.CourseTitle = data[6];
                    record.CourseStartDate = data[7];
                    record.CourseCompletionDate = data[8];
                    record.PersonId = data[9];
                    record.TrainingDeliveryType = data[10];
                    record.CreditDesignation = data[11];
                    record.TrainingSubType = data[12];
                    record.DutyHours = decimal.Parse(data[13]);
                    record.NonDutyHours = decimal.Parse(data[14]);
                    record.TrainingCredit = decimal.Parse(data[15]);
                    record.VendorName = data[16];
                    record.TrainingLocation = data[17];
                    record.RepaymentAgreementReqd = data[18];
                    record.ContServiceAgreementSigned = data[19];
                    record.TutionAndFees = decimal.Parse(data[20]);
                    record.TravelCosts = decimal.Parse(data[21]);
                    record.NongovtContrCost = decimal.Parse(data[22]);
                    record.MaterialCost = decimal.Parse(data[23]);
                    record.PerdiemCost = decimal.Parse(data[24]);
                    record.CreditType = data[25];
                    record.AccreditationIndicator = data[26];
                    record.PriorKnowledgeLevel = data[27];
                    record.ImpactOnPerformance = data[28];
                    record.RecommendTrngToOthers = data[29];
                    record.TrainingPartOfIdp = data[30];
                    record.PriorSupvApprovalReceived = data[31];
                    record.TypeOfPayment = data[32];
                    record.TrainingTravelIndicator = data[33];
                    record.CourseType = data[34];
                    record.CourseId = data[35];
                }
                OLURecords.Add(record);

            }
        }

    }//end class
}//end namespace