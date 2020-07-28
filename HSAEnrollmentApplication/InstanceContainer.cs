using System;
using Autofac;

namespace HSAEnrollmentApplication
{
    public static class InstanceContainer
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<EnrollmentInteractiveConsole>().As<IEnrollmentInteractiveConsole>();
            builder.RegisterType<CSVReader>().As<ICSVReader>();

            return builder.Build();
        }
    }
}
