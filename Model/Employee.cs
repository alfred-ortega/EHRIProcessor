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
        public List<TrainingRecord> TrainingRecords { get; set; }

        public Employee()
        {
            TrainingRecords = new List<TrainingRecord>();
        }

        public void ValidateTrainingRecords()
        {
            if (TrainingRecords.Count > 0)
            {
                TrainingRecords.Sort();
                removeDuplicates();
                List<TrainingRecord> cleanedList = new List<TrainingRecord>();
                foreach (TrainingRecord record in TrainingRecords)
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
            new_courseId = TrainingRecords[0].CourseId;
            List<TrainingRecord> cleanedList = new List<TrainingRecord>();
            foreach(TrainingRecord record in TrainingRecords)
            {
                new_courseId = record.CourseId;
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
