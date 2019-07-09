using System;
using System.Collections.Generic;
using System.Text;

namespace EHRIProcessor.Model
{
    class OLURecord : IComparable<OLURecord>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string TrainingPurposeType { get; set; }
        public string TrainingSourceType { get; set; }
        public string TrainingType { get; set; }
        public string TrainingTitle { get; set; }
        public DateTime TrainingStartDate { get; set; }
        public DateTime TrainingEndDate { get; set; }
        public string CHRISUserID { get; set; }
        public string TrainingDeliveryType { get; set; }
        public string TrainingCreditDesignationType { get; set; }
        public string TrainingSubType { get; set; }
        public string TrainingDutyHours { get; set; }
        public string TrainingNonDutyHours { get; set; }
        public string TrainingCredit { get; set; }
        public string VendorName { get; set; }
        public string TrainingLocation { get; set; }
        public string ContinuedServiceAgreementRequired { get; set; }
        public string ContinuedServiceAgreementExpDate { get; set; }
        public string TrainingTuitionandFeesCost { get; set; }
        public string TrainingTravelCost { get; set; }
        public string TrainingNonGovtContributionCost { get; set; }
        public string TrainingMaterialsCost { get; set; }
        public string TrainingPerDiemCost { get; set; }
        public string TrainingCreditType { get; set; }
        public string TrainingAccreditationIndicator { get; set; }
        public string PriorSubjectKnowledge { get; set; }
        public string ImpactonPerformance { get; set; }
        public string RecommendTrainingtoOthers { get; set; }
        public string TrainingPartOfIDP { get; set; }
        public string PriorSupervisoryApprovalReceived { get; set; }
        public string TypeOfPayment { get; set; }
        public string TravelIndicator { get; set; }
        public string CourseType { get; set; }
        public string CourseID { get; set; }

        public OLURecord()
        {
            FirstName = String.Empty;
            LastName = String.Empty;
            EmailAddress = String.Empty;
            TrainingPurposeType = String.Empty;
            TrainingSourceType = String.Empty;
            TrainingType = String.Empty;
            TrainingTitle = String.Empty;
            //TrainingStartDate = String.Empty;
            //TrainingEndDate = String.Empty;
            CHRISUserID = String.Empty;
            TrainingDeliveryType = String.Empty;
            TrainingCreditDesignationType = String.Empty;
            TrainingSubType = String.Empty;
            TrainingDutyHours = String.Empty;
            TrainingNonDutyHours = String.Empty;
            TrainingCredit = String.Empty;
            VendorName = String.Empty;
            TrainingLocation = String.Empty;
            ContinuedServiceAgreementRequired = String.Empty;
            ContinuedServiceAgreementExpDate = String.Empty;
            TrainingTuitionandFeesCost = String.Empty;
            TrainingTravelCost = String.Empty;
            TrainingNonGovtContributionCost = String.Empty;
            TrainingMaterialsCost = String.Empty;
            TrainingPerDiemCost = String.Empty;
            TrainingCreditType = String.Empty;
            TrainingAccreditationIndicator = String.Empty;
            PriorSubjectKnowledge = String.Empty;
            ImpactonPerformance = String.Empty;
            RecommendTrainingtoOthers = String.Empty;
            TrainingPartOfIDP = String.Empty;
            PriorSupervisoryApprovalReceived = String.Empty;
            TypeOfPayment = String.Empty;
            TravelIndicator = String.Empty;
            CourseType = String.Empty;
            CourseID = String.Empty;
        }

        public bool CheckIfValid()
        {
            bool retval = false;
            retval = checkCompletionDates();
            if (!retval)
                return retval;
            //Set Yes, No, NA values
            this.ContinuedServiceAgreementRequired = setYesNoNAFields(this.ContinuedServiceAgreementRequired);
            this.TrainingAccreditationIndicator = setYesNoNAFields(this.TrainingAccreditationIndicator);
            this.TravelIndicator = setYesNoNAFields(this.TravelIndicator);

            this.TrainingTitle = checkForSpecialCharactersInTitle(this.TrainingTitle);

            return retval;
        }

        private string checkForSpecialCharactersInTitle(string title)
        {
            string retval = title;
            retval = retval.Replace("&", "&amp;");
            retval = retval.Replace("<", "&lt;");
            retval = retval.Replace(">", "&gt;");
            return retval;
        }

        private bool checkCompletionDates()
        {
            return this.TrainingEndDate >= this.TrainingStartDate;
        }

        string setYesNoNAFields(string value)
        {
            string retval = "NA";
            if ((value == "Y") || (value == "N"))
                retval = value;

            return retval;
        }





        #region IComparable
        int IComparable<OLURecord>.CompareTo(OLURecord other)
        {
            int retval = this.CourseID.CompareTo(other.CourseID);
            if (retval == 0)
            {
                retval = this.TrainingEndDate.CompareTo(other.TrainingEndDate);
            }
            return retval;

            throw new NotImplementedException();
        }

        #endregion
    }
}
