﻿using System;
using HSAEnrollmentApplication.Utilities;

namespace HSAEnrollmentApplication
{
    public class Application : IApplication
    {
        IEnrollmentInteractiveConsole _enrollmentInteractiveConsole;

        public Application(IEnrollmentInteractiveConsole enrollmentInteractiveConsole)
        {
            _enrollmentInteractiveConsole = enrollmentInteractiveConsole;
        }

        public void RunApplication(string format)
        {
            _enrollmentInteractiveConsole.EnrollmentStartInteractiveConsole(format);

            _enrollmentInteractiveConsole.ReadCSV();

            _enrollmentInteractiveConsole.DisplayData();
        }
    }
}