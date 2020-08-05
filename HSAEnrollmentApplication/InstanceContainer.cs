using System;
using Autofac;
using HSAEnrollmentApplication.Utilities;

namespace HSAEnrollmentApplication
{
    public static class InstanceContainer
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EnrollmentCSVProgram>().As<IEnrollmentCSVProgram>();
            builder.RegisterType<EnrollmentCSV>().As<ICSVType>();
            builder.RegisterType<ReadCSVToDataTable>().UsingConstructor(typeof(ICSVType));
            builder.RegisterType<ReadCSVToDataTable>().As<IReadCSV>();
            //builder.RegisterType<Application>().As<IApplication>();
            //builder.RegisterType<EnrollmentInteractiveConsole>().As<IEnrollmentInteractiveConsole>();
            //builder.RegisterType<CSVReader>().As<ICSVReader>();

            return builder.Build();
        }
    }
}
