using System;
using System.Collections.Generic;
using System.Text;

namespace EHRIProcessor.Engine
{
    public class MainFrameCDPWriter
    {
        StringBuilder sb;
        int counter;

        public MainFrameCDPWriter()
        {
            setup();

        }

        public string Write(string[] filesToTransfer)
        {
            foreach (string fileToTransfer in filesToTransfer)
            {
                writeCopyInfo(fileToTransfer);
                counter++;
            }
            writeFooter();
            return sb.ToString();
        }

        public string Write(string fileToTransfer)
        {
            writeCopyInfo(fileToTransfer);
            writeFooter();
            return sb.ToString();
        }

        void setup()
        {
            sb = new StringBuilder();
            counter = 1;
            writeHeader();
        }

        void writeHeader()
        {
            sb.AppendLine("/*BEGIN_REQUESTER_COMMENTS");
            sb.AppendLine("    $PNODE$=\"CD.WIN.GSAKFC\" $PNODE_OS$=\"Windows\"");
            sb.AppendLine("    $SNODE$=\"NDM.OPM.WDPC\" $SNODE_OS$=\"OS/390\"");
            sb.AppendLine("    $OPTIONS$=\"\"");
            sb.AppendLine("  END_REQUESTER_COMMENTS*/");
            sb.AppendLine("");
            sb.AppendLine("EHRIPRD PROCESS ");
            sb.AppendLine("	SNODE=NDM.OPM.WDPC");
            sb.AppendLine("");

        }

        void writeCopyInfo(string fileName)
        {
            sb.AppendLine(string.Format("SEND{0} COPY",counter));
            sb.AppendLine("	FROM (");
            sb.AppendLine("        FILE =\\\\b04tcm - condir01.ent.ds.gsa.gov\\transfer\\outgoing\\" + fileName);
            sb.AppendLine("	)");
            sb.AppendLine("	TO (");
            sb.AppendLine("		FILE=/opm/EHRI/GS00/" + fileName);
            sb.AppendLine("		DISP=RPL");
            sb.AppendLine("	)");
            sb.AppendLine("	COMPRESS Extended");
            sb.AppendLine("");

        }

        void writeFooter()
        {
            sb.AppendLine("PEND");
        }
    }
}
