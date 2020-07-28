using System;
using Autofac;

namespace HSAEnrollmentApplication
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //All instnaces needed so top level control dependencies for application
            var container = InstanceContainer.ConfigureContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.RunApplication();
            }

            //EnrollmentInteractiveConsole consoleApp = new EnrollmentInteractiveConsole();
        
            //consoleApp.EnrollmentStartInteractiveConsole();
            
            //consoleApp.ReadCSV();

            //consoleApp.DisplayData();
            
            return;
        }
    }
}
