using System;
using System.Collections.Generic;

namespace EHRIProcessor.Model
{
    public partial class EhriStudHist
    {
        public string Emplid { get; set; }
        public DateTime Birthdate { get; set; }
        public string Ssn { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
