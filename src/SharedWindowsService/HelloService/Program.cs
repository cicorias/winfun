using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HelloService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

            var service = new Service1();
            
            if (!Environment.UserInteractive)
            {
                var servicesToRun = new ServiceBase[] { service };
                ServiceBase.Run(servicesToRun);
                return;
            }

            Console.WriteLine("Running as a Console Application");
            Console.WriteLine(" 1. Run Service");
            Console.WriteLine(" 2. Other Option");
            Console.WriteLine(" 3. Exit");
            Console.Write("Enter Option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Starting Service");
                    service.Start(args);
                    Console.WriteLine("Service Start Returned - Press Enter To Exit");
                    Console.ReadLine();
                    break;

                case "2":
                    // TODO!
                    break;
            }

            Console.WriteLine("Closing");

        }
    }
}
