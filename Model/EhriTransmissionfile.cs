using System;
using System.Collections.Generic;

namespace EHRIProcessor.Model
{
    public partial class EhriTransmissionfile
    {
        public string TransmissionFileId { get; set; }
        public string FileName { get; set; }
        public DateTime? SentToOpmdate { get; set; }
        public int? RecordCount { get; set; }
    }
}
