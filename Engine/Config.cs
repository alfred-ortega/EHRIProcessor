using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;

namespace EHRIProcessor.Engine
{
    public sealed class Config
    {
        private static readonly Config settings = new Config();

        public static Config Settings
        {
            get{return settings;}
            
        }
        private static IConfiguration AppSettings {get;set;}
        public string BaseDirectory = string.Empty;
        public string TransferDirectory = string.Empty;
        public string ArchiveDirectory = string.Empty;
        public string OluDB = string.Empty;
        public string HRLinksDB = string.Empty;

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
            OluDB = AppSettings.GetSection("AppSettings")["OluDB"];
            HRLinksDB = AppSettings.GetSection("AppSettings")["HRLinksDB"];


        }
    }//end class
}//end namespace