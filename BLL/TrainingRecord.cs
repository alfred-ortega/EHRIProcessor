using System;
using System.Collections.Generic;

namespace EHRIProcessor.Model
{
    public partial class TrainingRecord
    {
        public bool CheckIfValid()
        {
            return false;
        }
        private void validate()
        {
            bool trainingDatesValid = checkCompletionDates();
            if(trainingDatesValid)
            {
                checkForSpecialCharactersInTitle(this.CourseTitle);
                this.ContServiceAgreementSigned = setYesNoNAFields(this.ContServiceAgreementSigned);
                this.AccreditationIndicator = setYesNoNAFields(this.AccreditationIndicator);
                this.TrainingTravelIndicator = setYesNoNAFields(this.TrainingTravelIndicator);
            }
            else
            {
                this.ErrorMessage = "Completion date before start date";
                this.ProcessStatus = "X";
            }


        }

        private string checkForSpecialCharactersInTitle(string valueToTest)
        {
            string retval = valueToTest;
            retval = retval.Replace("&", "&amp;");
            retval = retval.Replace("<", "&lt;");
            retval = retval.Replace(">", "&gt;");
            return retval;
        }

        private bool checkCompletionDates()
        {
            return DateTime.Parse(this.CourseCompletionDate) >= DateTime.Parse(this.CourseStartDate);
        }

        string setYesNoNAFields(string value)
        {
            string retval = "NA";
            if ((value == "Y") || (value == "N"))
                retval = value;

            return retval;
        }        
    }//end class
}//end namespace