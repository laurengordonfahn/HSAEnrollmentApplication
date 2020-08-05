using System;
using HSAEnrollmentApplication.Utilities;

namespace HSAEnrollmentApplication
{
    public class Application : IApplication
    {
        IEnrollmentCSVProgram _enrollmentCSVProgram;

        public Application(IEnrollmentCSVProgram enrollmentCSVProgram)
        {
            _enrollmentCSVProgram = enrollmentCSVProgram;
        }

        public void RunApplication(string format)
        {
            _enrollmentCSVProgram.EnrollmentConsoleProgram(format);

            _enrollmentInteractiveConsole.ReadCSV();

            _enrollmentInteractiveConsole.DisplayData();
        }
        //IEnrollmentInteractiveConsole _enrollmentInteractiveConsole;

        //public Application(IEnrollmentInteractiveConsole enrollmentInteractiveConsole)
        //{
        //    _enrollmentInteractiveConsole = enrollmentInteractiveConsole;
        //}

        //public void RunApplication(string format)
        //{
        //    _enrollmentInteractiveConsole.EnrollmentStartInteractiveConsole(format);

        //    _enrollmentInteractiveConsole.ReadCSV();

        //    _enrollmentInteractiveConsole.DisplayData();
        //}
    }
}
