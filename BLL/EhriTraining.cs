using System;
using System.Collections.Generic;
using System.Linq;

namespace EHRIProcessor.Model
{
    public partial class EhriTraining
    {


        public void CheckIfValid()
        {
            try 
            {
                formatRecord();
                checkCompletionDates();
                TrainingDeliveryType = convertDescriptionToCode(TrainingRecordValues.Value.DeliveryType,TrainingDeliveryType,"Delivery Type");
                CreditType = convertDescriptionToCode(TrainingRecordValues.Value.CreditType,CreditType,"Credit Type");
                TrainingSource = convertDescriptionToCode(TrainingRecordValues.Value.SourceType,TrainingSource,"Source Type");
                CreditDesignation = convertDescriptionToCode(TrainingRecordValues.Value.DesignationType,CreditDesignation,"Designation Type");
                TrainingType = convertDescriptionToCode(TrainingRecordValues.Value.TrainingType,TrainingType,"Training Type");
                TrainingSubType = convertDescriptionToCode(TrainingRecordValues.Value.TrainingSubType,TrainingSubType,"Training Sub-Type");
                TrainingPurpose = convertDescriptionToCode(TrainingRecordValues.Value.PurposeType,TrainingPurpose,"Purpose Type");
                ProcessStatus = "R";
            }
            catch(Exception x)
            {
                ErrorMessage = x.Message;
                ProcessStatus = "X";
            }
        }

#region "Validations"
        private void checkCompletionDates()
        {          
            if(DateTime.Parse(CourseCompletionDate) < DateTime.Parse(CourseStartDate))
            {
                throw new Exception("Course Completion date must be after Course Start date.");
            }
        }

        string convertDescriptionToCode(Dictionary<string,string> validValues, string testValue, string fieldName)
        {
            int i = 0;
            var arrayOfValues = validValues.Values.ToArray();
            string code = string.Empty;
            foreach(KeyValuePair<string,string> validValue in validValues)
            {
                if(validValue.Value == testValue)
                {
                    i++;
                    code = validValue.Key;
                }

            }
            if(i == 0)
            {
                throw new Exception(string.Format("Invalid value of {0} for {1}",testValue,fieldName));
            }
            return code;
        }
        


#endregion










#region "Format"
        private void formatRecord()
        {
             checkTitleForSpecialCharactersInTitle();
             ContServiceAgreementSigned = setYesNoNAFields(ContServiceAgreementSigned);
             AccreditationIndicator = setYesNoNAFields(AccreditationIndicator);
             TrainingTravelIndicator = setYesNoNAFields(TrainingTravelIndicator);
        }

        private void checkTitleForSpecialCharactersInTitle()
        {
            string testValue = CourseTitle;
            testValue = testValue.Replace("&", "&amp;");
            testValue = testValue.Replace("<", "&lt;");
            testValue = testValue.Replace(">", "&gt;");
            CourseTitle = testValue;
        }





        string setYesNoNAFields(string value)
        {
            string retval = "NA";
            if ((value == "Y") || (value == "N"))
                retval = value;

            return retval;
        }

#endregion

    }//end class
}//end namespace