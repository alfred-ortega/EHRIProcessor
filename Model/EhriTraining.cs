using System;
using System.Collections.Generic;

namespace EHRIProcessor.Model
{
    public partial class EhriTraining
    {
        public int EhriTrainingId { get; set; }
        public string TrainingFileId { get; set; }
        public string TransmissionFileId { get; set; }
        public string PersonId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string ErrorMessage { get; set; }
        public string ProcessStatus { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeMiddleName { get; set; }
        public string EmailAddress { get; set; }
        public string AgencySubElement { get; set; }
        public string TrainingPurpose { get; set; }
        public string TrainingSource { get; set; }
        public string TrainingType { get; set; }
        public string CourseTitle { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseCompletionDate { get; set; }
        public string TrainingDeliveryType { get; set; }
        public string CreditDesignation { get; set; }
        public string TrainingSubType { get; set; }
        public decimal? TutionAndFees { get; set; }
        public decimal? DutyHours { get; set; }
        public decimal? NonDutyHours { get; set; }
        public decimal? TravelCosts { get; set; }
        public decimal? NongovtContrCost { get; set; }
        public decimal? MaterialCost { get; set; }
        public decimal? PerdiemCost { get; set; }
        public decimal? TrainingCredit { get; set; }
        public string VendorName { get; set; }
        public string TrainingLocation { get; set; }
        public string AccreditationIndicator { get; set; }
        public string CreditType { get; set; }
        public string ContServiceAgreementSigned { get; set; }
        public string RepaymentAgreementReqd { get; set; }
        public string PriorKnowledgeLevel { get; set; }
        public string ImpactOnPerformance { get; set; }
        public string RecommendTrngToOthers { get; set; }
        public string PriorSupvApprovalReceived { get; set; }
        public string TrainingPartOfIdp { get; set; }
        public string TypeOfPayment { get; set; }
        public string TrainingTravelIndicator { get; set; }
        public string CourseType { get; set; }
        public string CourseId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Ssn { get; set; }
    }
}
