using System;

namespace HSAEnrollmentApplication
{
    class Program
    {
        
        static void Main(string[] args)
        {
            EnrollmentInteractiveConsole consoleApp = new EnrollmentInteractiveConsole();
        
            consoleApp.EnrollmentStartInteractiveConsole();
            
            consoleApp.ReadCSV();

            consoleApp.DisplayData();
            

            return;
        }
    }
}
