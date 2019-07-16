using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EHRIProcessor.Model;

namespace EHRIProcessor.Engine
{
    class XMLWriter
    {
        StringBuilder sb = new StringBuilder();
        string dateFormat = "yyyy-MM-dd";

        public XMLWriter()
        {

        }

        public void WriteEhriFile(List<EhriTraining> records, string fileName)
        {
            writeHeader();
            writeBody(records);
            writeFooter();
            writeFile(fileName);
        }

        void writeHeader()
        {
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<TrainingExport fileDate=\"" + DateTime.Now.ToString(dateFormat)  + "\" fileSource=\"GSAOLU\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"http://ehr.opm.gov/schemas/training/EHRITraining_v4_0.xsd\">");
        }


        void writeBody(List<EhriTraining> records)
        {
                foreach (EhriTraining record in records) 
            {
                sb.AppendLine("<TrainingRecord>");
                sb.AppendLine(writeNode("A", "RecordAction"));

                sb.AppendLine("<Employee>");
                sb.AppendLine(writeNode(record.Ssn, "SSN"));
                sb.AppendLine(writeNode(record.BirthDate.ToString(dateFormat), "BirthDate"));
                sb.AppendLine(writeNode(string.Empty, "EHRIEmployeeId"));
                //sb.AppendLine(writeNode(record.AgencySubElement, "AgencySubelement"));
                sb.AppendLine("</Employee>");

                sb.AppendLine(writeNode(record.CourseTitle,"TrainingTitle"));
                sb.AppendLine(writeNode(record.TrainingType,"TrainingTypeCode"));
                sb.AppendLine(writeNode(record.TrainingSubType,"TrainingSubTypeCode"));
                sb.AppendLine(writeNode(record.CourseStartDate,"TrainingStartDate"));
                sb.AppendLine(writeNode(record.CourseCompletionDate, "TrainingEndDate"));

                sb.AppendLine("<ContinuedServiceAgreement>");
                sb.AppendLine(writeNode(record.ContServiceAgreementSigned, "AgreementRequiredInd"));
                sb.AppendLine("</ContinuedServiceAgreement>");

                sb.AppendLine(writeNode(record.AccreditationIndicator, "AccreditationInd"));

                sb.AppendLine("<TrainingCredit>");
                sb.AppendLine(writeNode(record.TrainingCredit.ToString(), "CreditAmt"));
                sb.AppendLine(writeNode(record.CreditDesignation, "DesignationType"));
                sb.AppendLine(writeNode(record.CreditType, "CreditType"));
                sb.AppendLine("</TrainingCredit>");

                sb.AppendLine(writeNode(record.DutyHours.ToString(), "TrainingDutyHours"));
                sb.AppendLine(writeNode(record.NonDutyHours.ToString(), "TrainingNonDutyHours"));
                sb.AppendLine(writeNode(record.TrainingDeliveryType, "TrainingDeliveryTypeCode"));
                sb.AppendLine(writeNode(record.TrainingPurpose, "TrainingPurposeTypeCode"));
                sb.AppendLine(writeNode(record.TrainingSource, "TrainingSourceTypeCode"));

                sb.AppendLine("<TrainingCost>");
                sb.AppendLine(writeNode(record.MaterialCost.ToString(), "MaterialsCost"));
                sb.AppendLine(writeNode(record.PerdiemCost.ToString(), "PerDiemCost"));
                sb.AppendLine(writeNode(record.TravelCosts.ToString(), "TravelCost"));
                sb.AppendLine(writeNode(record.TutionAndFees.ToString(), "TuitionAndFees"));
                sb.AppendLine(writeNode(record.NongovtContrCost.ToString(), "NonGovernmentContribution"));
                sb.AppendLine("</TrainingCost>");

                sb.AppendLine(writeNode(record.TrainingTravelIndicator, "TrainingTravelIndicator"));
                sb.AppendLine("</TrainingRecord>");
            }
        }

        string writeNode(string value, string nodeName)
        {
            return string.Format("<{0}>{1}</{0}>", nodeName, value);
        }


        void writeFooter()
        {
            sb.Append("</TrainingExport>");
        }

        void writeFile(string path)
        {
            File.WriteAllText(path, sb.ToString());
        }

    }
}
