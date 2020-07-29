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

            string format = args.Length == 1 ? args[0] : "MMddyyyy";
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.RunApplication(format);
            }
            
            return;
        }
    }
}
