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
            
            var enrollmentType = new EnrollmentCSV();
            //builder.RegisterInstance(enrollmentType).As<ICSVType>();
            builder.Register(c => new ReadCSVToDataTable(enrollmentType)).As<IReadCSV>();
            //builder.RegisterType<ReadCSVToDataTable>().UsingConstructor(typeof(ICSVType)).As<IReadCSV>();

            //builder.RegisterType<Application>().As<IApplication>();
            //builder.RegisterType<EnrollmentInteractiveConsole>().As<IEnrollmentInteractiveConsole>();
            //builder.RegisterType<CSVReader>().As<ICSVReader>();

            return builder.Build();
        }
    }
}
