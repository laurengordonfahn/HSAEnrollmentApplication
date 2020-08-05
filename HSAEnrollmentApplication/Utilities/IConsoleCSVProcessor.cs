using System;

namespace HSAEnrollmentApplication.Utilities
{
    public interface IConsoleCSVProcessor
    {
        void WelcomeMessage(string welcomeMsg);
        string GetCSVPath();
        DateTime GetProcessDate(bool shouldRequestProcessingDate, string format);
        
    }
}