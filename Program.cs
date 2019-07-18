using System;
using System.Collections.Generic;
using System.IO;
using EHRIProcessor.Engine;
using EHRIProcessor.Model;
using Newtonsoft.Json;

namespace EHRIProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Logger.Log.Record("Beginning daily run.");
                Core core = new Core();
                core.Execute();
                Logger.Log.Record("End daily run.");
            }   
            catch(Exception x)
            {
                Logger.Log.Record(LogType.Error, x.ToString());
            }      
        }
    }
}
