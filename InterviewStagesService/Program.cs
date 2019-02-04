using System;
using System.ServiceProcess;

namespace Ualium.Candidate.Interview.InterviewStagesService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var servicesToRun = new ServiceBase[]
           {
                new ServiceContainer()
           };

            if (Environment.UserInteractive)
            {
                HexMaster.Helper.Run(servicesToRun);
            }
            else
            {
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}