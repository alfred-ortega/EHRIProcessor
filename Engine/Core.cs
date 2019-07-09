using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;

namespace EHRIProcessor.Engine
{
    class Core
    {
        Config config;

        public Core()
        {
            initConfig();
        }

        private void initConfig()
        {
            config = new Config();
        }

        public void Execute()
        {
            
        }


    }//end class
}//end namespace