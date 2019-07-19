using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;

namespace EHRIProcessor.Engine
{
    /// <summary>
    /// The config class provides a wrapper for the appsettings.json file. It's done as a singletone so that it can be statically called
    /// with no need to instantiate anyting
    /// 
    /// Example:  string dir = Config.Settings.BaseDirectory;
    /// </summary>
    public sealed class Config
    {
        private static readonly Config settings = new Config();

        public static Config Settings
        {
            get{return settings;}
            
        }
        private static IConfiguration AppSettings {get;set;}
        /// <summary>
        /// The base directory is where the OLU files will be picked up from to begin the loadig process.
        /// </summary>
        public string BaseDirectory = string.Empty;
        /// <summary>
        /// The transfer directory is where the files to be sent to EHRI will be stored.
        /// </summary>
        public string TransferDirectory = string.Empty;
        /// <summary>
        /// The archive directory is where the training files that have been parsed and stored will be saved.
        /// </summary>
        public string ArchiveDirectory = string.Empty;
         /// <summary>
         /// The Log directory is where the application logs will written out.
         /// </summary>
         public string LogDirectory = string.Empty;

        /// <summary>
        /// The config directory is where the ehri value config is stored
        /// </summary>
         public string ConfigDirectory = string.Empty;
        /// <summary>
        /// The connection string for the OLU schema in the MySQL database.
        /// </summary>
        public string OluDB = string.Empty;
        /// <summary>
        /// The connection string for the HRLinks schema in the MySQL database.
        /// </summary>
        public string HRLinksDB = string.Empty;

        public string CopyFile = string.Empty;

        static Config()
        {}

        private Config()
        {

            var builder = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json");

            AppSettings = builder.Build();
            BaseDirectory = AppSettings.GetSection("AppSettings")["BaseDirectory"];
            TransferDirectory = AppSettings.GetSection("AppSettings")["TransferDirectory"];
            ArchiveDirectory = AppSettings.GetSection("AppSettings")["ArchiveDirectory"];
            LogDirectory = AppSettings.GetSection("AppSettings")["LogDirectory"];
            OluDB = AppSettings.GetSection("AppSettings")["OluDB"];
            HRLinksDB = AppSettings.GetSection("AppSettings")["HRLinksDB"];
            CopyFile = AppSettings.GetSection("AppSettings")["CopyFile"];

        }
    }//end class
}//end namespace