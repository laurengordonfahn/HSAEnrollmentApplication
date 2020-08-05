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
            builder.RegisterType<ConsoleCSVProcessor>().As<IConsoleCSVProcessor>();
            var enrollmentType = new EnrollmentCSV();
            builder.RegisterInstance(enrollmentType).As<ICSVType>();
            builder.Register(c => new ReadCSVToDataTable(c.Resolve<ICSVType>())).As<IReadCSV>();

            return builder.Build();
        }
    }
}
