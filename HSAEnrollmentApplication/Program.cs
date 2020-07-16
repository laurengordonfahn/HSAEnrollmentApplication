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
            // Test acceptance criteria and generate return collection

            return;
        }
    }
}
