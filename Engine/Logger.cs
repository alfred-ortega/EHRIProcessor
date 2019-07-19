using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Collections.Generic;

namespace EHRIProcessor.Engine
{
    /// <summary>
    /// The config class provides a wrapper for the appsettings.json file. It's done as a singleton so that it can be statically called
    /// with no need to instantiate anyting
    /// 
    /// Example:  string dir = Config.Settings.BaseDirectory;
    /// </summary>
    public sealed class Logger
    {
        private static readonly Logger log = new Logger();
        private static string logFileName = Config.Settings.LogDirectory + DateTime.Now.ToString("yyyy-MM-dd") + "_log.txt";
        public static Logger Log
        {
            get{return log;}
        }



        public void Record(string entry)
        {
            Record(LogType.Status,entry);
        }

        public void Record(LogType logType, string entry)
        {
            string ts = DateTime.Now.ToLongTimeString();
            string lType = string.Empty;
            if(logType == LogType.Error)
            {
                lType = "Error";
            }
            else
            {
                lType = "Status";
                System.Console.WriteLine(ts + " " + entry);
            }
            string message = string.Format("{0}\t{1}\t{2}",ts,lType,entry);
            using(StreamWriter streamWriter = new StreamWriter(logFileName,true))
            {
                streamWriter.WriteLineAsync(message);
                streamWriter.Close();
            }

        }
        
    }//end class
}//end namespace