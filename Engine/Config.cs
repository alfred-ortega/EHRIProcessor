using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;

namespace EHRIProcessor.Engine
{
    public class Config
    {
        private static IConfiguration Settings {get;set;}
        public string BaseDirectory = string.Empty;
        public string TransferDirectory = string.Empty;
        public string ArchiveDirectory = string.Empty;
        public string OluDB = string.Empty;
        public string HRLinksDB = string.Empty;

        public Config()
        {
            var builder = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json");

            Settings = builder.Build();
            BaseDirectory = Settings.GetSection("AppSettings")["BaseDirectory"];
            TransferDirectory = Settings.GetSection("AppSettings")["TransferDirectory"];
            ArchiveDirectory = Settings.GetSection("AppSettings")["ArchiveDirectory"];
            OluDB = Settings.GetSection("AppSettings")["OluDB"];
            HRLinksDB = Settings.GetSection("AppSettings")["HRLinksDB"];


        }
    }//end class
}//end namespace