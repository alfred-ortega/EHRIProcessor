﻿using System;
using System.Collections.Generic;

namespace EHRIProcessor.Model
{
    public partial class EhriStudHist
    {
        public string Emplid { get; set; }
        public DateTime Birthdate { get; set; }
        public string Ssn { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }                        
        public DateTime? UpdatedAt { get; set; }

        public DateTime? CreatedAt { get; set; }


    }
}
