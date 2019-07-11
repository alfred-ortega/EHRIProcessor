using System;
using System.Collections.Generic;

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
                validFieldFromList(TrainingRecordValues.Value.DeliveryType,TrainingDeliveryType,"Delivery Type");
                validFieldFromList(TrainingRecordValues.Value.CreditType,CreditType,"Credit Type");
                validFieldFromList(TrainingRecordValues.Value.SourceType,TrainingSource,"Source Type");
                validFieldFromList(TrainingRecordValues.Value.DesignationType,CreditDesignation,"Designation Type");
                validFieldFromList(TrainingRecordValues.Value.TrainingType,TrainingType,"Training Type");
                validFieldFromList(TrainingRecordValues.Value.TrainingSubType,TrainingSubType,"Training Sub-Type");
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

        void validFieldFromList(string[] validValues, string testValue, string fieldName)
        {
            int i = Array.IndexOf(validValues,testValue);
            if(i == -1)
            {
                throw new Exception(string.Format("Invalid value of {0} for {1}",testValue,fieldName));
            }

        }

        


#endregion










#region "Format"
        private void formatRecord()
        {
            // checkForSpecialCharactersInTitle(this.CourseTitle);
            // this.ContServiceAgreementSigned = setYesNoNAFields(this.ContServiceAgreementSigned);
            // this.AccreditationIndicator = setYesNoNAFields(this.AccreditationIndicator);
            // this.TrainingTravelIndicator = setYesNoNAFields(this.TrainingTravelIndicator);
        }

        private string checkForSpecialCharactersInTitle(string valueToTest)
        {
            string retval = valueToTest;
            retval = retval.Replace("&", "&amp;");
            retval = retval.Replace("<", "&lt;");
            retval = retval.Replace(">", "&gt;");
            return retval;
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