using System;
using System.Globalization;

namespace HSAEnrollmentApplication.Utilities
{
    public class ConsoleCSVProcessor : IConsoleCSVProcessor
    {
        public DateTime ProcessDate = System.DateTime.UtcNow.Date;

        public ConsoleCSVProcessor()
        {
        }

        /// <summary>
        /// Welcome message for program
        /// </summary>
        public void WelcomeMessage(string welcomeMsg)
        {

            Console.WriteLine(welcomeMsg);

            return;
        }

        /// <summary>
        /// Console Prompts for getting the csv path
        /// </summary>
        public string GetCSVPath()
        {
            Console.WriteLine("Please, enter the local path of the csv file you would like processed.");

            string csvPath = Console.ReadLine();
            return csvPath;
        }

        /// <summary>
        /// Console prompts and processing of a date to process the csv data file against.
        /// </summary>
        public DateTime GetProcessDate(bool shouldRequestProcessingDate, string format)
        {
            if (shouldRequestProcessingDate)
            {
                Console.WriteLine("At this time you may enter the date on which you would like this enrollment data to be processed against in " + format + " format " +
                    "OR " +
                    "you can press return and UTC aka GMT will be used.");

                string submittedDate = Console.ReadLine();
                bool isDateValid = false;

                while (!isDateValid)
                {
                    if (String.IsNullOrEmpty(submittedDate))
                    {
                        return ProcessDate;
                    }
                    else
                    {
                        DateTime dateOutput;

                        if (DateTime.TryParseExact(submittedDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput))
                        {
                            DateTime processDate = dateOutput;
                            return processDate;
                        }
                        else
                        {
                            Console.WriteLine("The date you entered is either not a valid date or not in the format" + format + ", please try again or simply press the return/enter key to use the default GMT");
                            submittedDate = Console.ReadLine();
                        }
                    }

                }
            }
            return ProcessDate;
        }
    }
}
