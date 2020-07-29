namespace HSAEnrollmentApplication
{
    public interface IEnrollmentInteractiveConsole
    {
        void DisplayData();
        void EnrollmentStartInteractiveConsole(string arg);
        void GetCSVPath();
        void GetProcessDate();
        void ReadCSV();
    }
}