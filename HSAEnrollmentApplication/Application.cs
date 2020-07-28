using System;
namespace HSAEnrollmentApplication
{
    public class Application : IApplication
    {
        IEnrollmentInteractiveConsole _enrollmentInteractiveConsole;

        public Application(IEnrollmentInteractiveConsole enrollmentInteractiveConsole)
        {
            _enrollmentInteractiveConsole = enrollmentInteractiveConsole;
        }

        public void RunApplication()
        {
            _enrollmentInteractiveConsole.EnrollmentStartInteractiveConsole();

            _enrollmentInteractiveConsole.ReadCSV();

            _enrollmentInteractiveConsole.DisplayData();
        }
    }
}
