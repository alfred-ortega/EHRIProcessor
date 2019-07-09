using System;
using System.Collections.Generic;

namespace EHRIProcessor.Model
{
    public partial class TrainingFileInfo
    {
        public string TrainingFileInfoId { get; set; }
        public string FileName { get; set; }
        public DateTime? Loaded { get; set; }
        public int? FileRecordCount { get; set; }
        public int? SavedRecordCount { get; set; }
    }
}
