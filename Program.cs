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
               Core core = new Core();
               core.Execute();
            }   
            catch(Exception x)
            {
                Console.WriteLine(x.ToString());
            }      
        }



    }
}
