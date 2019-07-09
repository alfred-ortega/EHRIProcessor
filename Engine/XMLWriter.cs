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

        public XMLWriter(List<Employee> records)
        {
            writeHeader();
            writeBody(records);
            writeFooter();

        }

        void writeHeader()
        {
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<TrainingExport fileDate=\"2018-03-01\" fileSource=\"GSAOLU\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"http://ehr.opm.gov/schemas/training/EHRITraining_v4_0.xsd\">");
        }


        void writeBody(List<Employee> records)
        {
            foreach (Employee emp in records)
            {
                emp.ValidateTrainingRecords();

                foreach (OLURecord olu in emp.TrainingRecords)
                {
                    sb.AppendLine("<TrainingRecord>");
                    sb.AppendLine(writeNode("A", "RecordAction"));
                    //Employee
                    sb.AppendLine("<Employee>");
                    sb.AppendLine(writeNode(emp.SSN, "SSN"));
                    sb.AppendLine(writeNode(emp.DateOfBirth.ToString(dateFormat), "BirthDate"));
                    sb.AppendLine(writeNode(string.Empty, "EHRIEmployeeId"));
                    sb.AppendLine(writeNode(emp.AgencySubElement, "AgencySubelement"));
                    sb.AppendLine("</Employee>");

                    sb.AppendLine(writeNode(olu.TrainingTitle,"TrainingTitle"));
                    sb.AppendLine(writeNode(olu.TrainingType,"TrainingTypeCode"));
                    sb.AppendLine(writeNode(olu.TrainingSubType,"TrainingSubTypeCode"));
                    sb.AppendLine(writeNode(olu.TrainingStartDate.ToString(dateFormat),"TrainingStartDate"));
                    sb.AppendLine(writeNode(olu.TrainingEndDate.ToString(dateFormat), "TrainingEndDate"));

                    sb.AppendLine("<ContinuedServiceAgreement>");
                    sb.AppendLine(writeNode("NA", "AgreementRequiredInd"));
                    sb.AppendLine("</ContinuedServiceAgreement>");

                    sb.AppendLine(writeNode(olu.TrainingAccreditationIndicator, "AccreditationInd"));

                    sb.AppendLine("<TrainingCredit>");
                    sb.AppendLine(writeNode(olu.TrainingCredit, "CreditAmt"));
                    sb.AppendLine(writeNode(olu.TrainingCreditDesignationType, "DesignationType"));
                    sb.AppendLine(writeNode(olu.TrainingCreditType, "CreditType"));
                    sb.AppendLine("</TrainingCredit>");

                    sb.AppendLine(writeNode(olu.TrainingDutyHours, "TrainingDutyHours"));
                    sb.AppendLine(writeNode("0", "TrainingNonDutyHours"));
                    sb.AppendLine(writeNode(olu.TrainingDeliveryType, "TrainingDeliveryTypeCode"));
                    sb.AppendLine(writeNode(olu.TrainingPurposeType, "TrainingPurposeTypeCode"));
                    sb.AppendLine(writeNode(olu.TrainingSourceType, "TrainingSourceTypeCode"));

                    sb.AppendLine("<TrainingCost>");
                    sb.AppendLine(writeNode("0", "MaterialsCost"));
                    sb.AppendLine(writeNode("0", "PerDiemCost"));
                    sb.AppendLine(writeNode("0", "TravelCost"));
                    sb.AppendLine(writeNode("0", "TuitionAndFees"));
                    sb.AppendLine(writeNode("0", "NonGovernmentContribution"));
                    sb.AppendLine("</TrainingCost>");

                    sb.AppendLine(writeNode("N", "TrainingTravelIndicator"));
                    sb.AppendLine("</TrainingRecord>");

                }
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

        public void WriteFile(string path)
        {
            File.WriteAllText(path, sb.ToString());
        }

    }
}
