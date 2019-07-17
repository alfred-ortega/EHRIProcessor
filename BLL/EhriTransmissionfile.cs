using System;
using System.Collections.Generic;

namespace EHRIProcessor.Model
{
    public partial class EhriTransmissionfile
    {

        public EhriTransmissionfile()
        {
            setFileName();
            setFileId();
        }

        void setFileName()
        {
            string lastDayOfPreviousMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1).ToString("yyyMMdd");
            FileName = string.Format("TX{0}GS000_4_0.xml",lastDayOfPreviousMonth);            
        }

        void setFileId()
        {
            this.TransmissionFileId = Guid.NewGuid().ToString();        
        }


    }//end class
}//end namespace