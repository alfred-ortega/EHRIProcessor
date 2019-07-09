using System;
using System.Collections.Generic;
using System.Text;

namespace EHRIProcessor.Model
{
    class Employee
    {
        public string Name { get; set; }
        public string SSN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmployeeID { get; set; }
        public string AgencySubElement { get; set; }
        public List<OLURecord> TrainingRecords { get; set; }

        public Employee()
        {
            TrainingRecords = new List<OLURecord>();
        }

        public void ValidateTrainingRecords()
        {
            if (TrainingRecords.Count > 0)
            {
                TrainingRecords.Sort();
                removeDuplicates();
                List<OLURecord> cleanedList = new List<OLURecord>();
                foreach (OLURecord record in TrainingRecords)
                {
                    if(record.CheckIfValid())
                    {
                        cleanedList.Add(record);
                    }
                }
                TrainingRecords = cleanedList;
            }
        }


        void removeDuplicates()
        {
            string old_courseId, new_courseId;
            old_courseId = string.Empty;
            new_courseId = TrainingRecords[0].CourseID;
            List<OLURecord> cleanedList = new List<OLURecord>();
            foreach(OLURecord record in TrainingRecords)
            {
                new_courseId = record.CourseID;
                if (new_courseId != old_courseId)
                {
                    cleanedList.Add(record);
                }
                old_courseId = new_courseId;
            }
            TrainingRecords = cleanedList;

        }
    }
}
