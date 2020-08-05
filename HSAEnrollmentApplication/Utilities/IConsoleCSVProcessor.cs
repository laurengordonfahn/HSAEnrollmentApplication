namespace HSAEnrollmentApplication.Utilities
{
    public interface IConsoleCSVProcessor
    {
        void GetCSVPath();
        void GetProcessDate();
        void WelcomeMessage(string format);
    }
}