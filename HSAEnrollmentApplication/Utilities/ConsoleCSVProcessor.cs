using System;
using System.Globalization;

namespace HSAEnrollmentApplication.Utilities
{
    public class ConsoleCSVProcessor : IConsoleCSVProcessor
    {
        public string WelcomeMsg = "Welcome to the Enrollment Application interactive console.";
        public bool ShouldRequestProcessingDate = true;
        public string CSVPath;
        public DateTime ProcessDate = System.DateTime.UtcNow.Date;
        public string DateFormat;

        public ConsoleCSVProcessor()
        {
        }

        /// <summary>
        /// Welcome message for program
        /// </summary
        public void WelcomeMessage(string format)
        {

            Console.WriteLine(WelcomeMsg);

            return;
        }

        /// <summary>
        /// Console Prompts for getting the csv path
        /// </summary>
        public void GetCSVPath()
        {
            Console.WriteLine("Please, enter the local path of the csv file you would like processed.");

            CSVPath = Console.ReadLine();
            return;
        }

        /// <summary>
        /// Console prompts and processing of a date to process the csv data file against.
        /// </summary>
        public void GetProcessDate()
        {
            if (ShouldRequestProcessingDate)
            {
                Console.WriteLine("At this time you may enter the date on which you would like this enrollment data to be processed against in " + DateFormat + " format " +
                    "OR" +
                    "you can press return and UTC aka GMT will be used.");

                string submittedDate = Console.ReadLine();
                bool isDateValid = false;

                while (!isDateValid)
                {
                    if (String.IsNullOrEmpty(submittedDate))
                    {
                        return;
                    }
                    else
                    {
                        DateTime dateOutput;

                        if (DateTime.TryParseExact(submittedDate, DateFormat, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput))
                        {
                            ProcessDate = dateOutput;
                            return;
                        }
                        else
                        {
                            Console.WriteLine("The date you entered is either not a valid date or not in the format" + DateFormat + ", please try again or simply press the return/enter key to use the default GMT");
                            submittedDate = Console.ReadLine();
                        }
                    }

                }
            }
            return;
        }
    }
}
