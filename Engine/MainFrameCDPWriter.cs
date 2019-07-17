using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EHRIProcessor.Engine
{
    /// <summary>
    /// Each file that is sent to the OPM mainframe must be identified in an "copy" file with mainframe instructions.
    /// The MainFrameCDPWriter writes out those instructions for each file being submitted to OPM.
    /// </summary>
    public class MainFrameCDPWriter
    {
        StringBuilder sb;
        int counter;

        public MainFrameCDPWriter()
        {
            setup();

        }

        public void Write()
        {
            try
            {
                string[] filesToTransfer = Directory.GetFiles(Config.Settings.TransferDirectory);
                string body = buildCopyFile(filesToTransfer);
                File.WriteAllText(Config.Settings.TransferDirectory + Config.Settings.CopyFile,body);
            }
            catch (System.Exception x)
            {
                Console.WriteLine("Failed to write mainframe copy file.\r\n" + x.ToString());
            }
        }

        private string buildCopyFile(string[] filesToTransfer)
        {
            foreach (string fileToTransfer in filesToTransfer)
            {
                string tempFileName = fileToTransfer.Replace(Config.Settings.TransferDirectory,string.Empty);
                createCopyInfo(tempFileName);
                counter++;
            }
            createFooter();
            return sb.ToString();
        }

        void setup()
        {
            sb = new StringBuilder();
            counter = 1;
            createHeader();
        }

        void createHeader()
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

        void createCopyInfo(string fileName)
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

        void createFooter()
        {
            sb.AppendLine("PEND");
        }
    }
}
