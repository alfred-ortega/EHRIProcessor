using System;
using System.IO;
using System.Collections.Generic;
using EHRIProcessor.Model;

namespace EHRIProcessor.Engine
{
    class TrainingRecordLoader
    {
        public List<OLURecord> OLURecords;

        public TrainingRecordLoader()
        {
            OLURecords = new List<OLURecord>();

        }

        public void Load(string trainingFile)
        {
            try 
            {
                string[] trainingEntries = File.ReadAllLines(trainingFile);
                loadOluRecords(trainingEntries);

            }
            catch(Exception x)
            {
                Console.WriteLine(x.ToString());
            }
        }


        void loadOluRecords(string[] trainingEntries)
        {
            foreach (string trainingEntry in trainingEntries)
            {
                OLURecord record = new OLURecord();
                string[] data = trainingEntry.Split("~");
                for (int i = 0; i < data.Length; i++)
                {
                    record.FirstName = data[0];
                    record.LastName = data[1];
                    record.EmailAddress = data[2];
                    record.TrainingPurposeType = data[3];
                    record.TrainingSourceType = data[4];
                    record.TrainingType = data[5];
                    record.TrainingTitle = data[6];
                    record.TrainingStartDate = DateTime.Parse(data[7]);
                    record.TrainingEndDate = DateTime.Parse(data[8]);
                    record.CHRISUserID = data[9];
                    record.TrainingDeliveryType = data[10];
                    record.TrainingCreditDesignationType = data[11];
                    record.TrainingSubType = data[12];
                    record.TrainingDutyHours = data[13];
                    record.TrainingNonDutyHours = data[14];
                    record.TrainingCredit = data[15];
                    record.VendorName = data[16];
                    record.TrainingLocation = data[17];
                    record.ContinuedServiceAgreementRequired = data[18];
                    record.ContinuedServiceAgreementExpDate = data[19];
                    record.TrainingTuitionandFeesCost = data[20];
                    record.TrainingTravelCost = data[21];
                    record.TrainingNonGovtContributionCost = data[22];
                    record.TrainingMaterialsCost = data[23];
                    record.TrainingPerDiemCost = data[24];
                    record.TrainingCreditType = data[25];
                    record.TrainingAccreditationIndicator = data[26];
                    record.PriorSubjectKnowledge = data[27];
                    record.ImpactonPerformance = data[28];
                    record.RecommendTrainingtoOthers = data[29];
                    record.TrainingPartOfIDP = data[30];
                    record.PriorSupervisoryApprovalReceived = data[31];
                    record.TypeOfPayment = data[32];
                    record.TravelIndicator = data[33];
                    record.CourseType = data[34];
                    record.CourseID = data[35];
                }
                OLURecords.Add(record);

            }
        }

    }//end class
}//end namespace