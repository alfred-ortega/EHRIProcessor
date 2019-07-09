using System;
using EHRIProcessor.Engine;
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
