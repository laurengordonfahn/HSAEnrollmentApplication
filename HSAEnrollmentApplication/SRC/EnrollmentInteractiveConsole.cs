using System;
using System.Globalization;

namespace HSAEnrollmentApplication
{
    public class EnrollmentInteractiveConsole
    {
        public string WelcomeMsg = "Welcome to the Enrollment Application interactive console.";
        public bool shouldRequestProcessingDate = true;

        public void EnrollmentStartInteractiveConsole()
        {
            Console.WriteLine(WelcomeMsg);
            Console.WriteLine("Please, enter the local path of the csv file you would like processed.");

            string path = Console.ReadLine();

            if (shouldRequestProcessingDate)
            {
                Console.WriteLine("At this time you may enter the date on which you would like this enrollment data to be processed against in mmddyyyy format " +
                    "OR" +
                    "you can press return and UTC aka GMT will be used.");

                string processDate = Console.ReadLine();
                bool isDateValid = false;

                while (!isDateValid)
                {
                    if (String.IsNullOrEmpty(processDate))
                    {
                        isDateValid = true;
                    }
                    else
                    {
                        DateTime dateOutput;
                        string format = "MMddyyyy";

                        Console.WriteLine(processDate);

                        if (DateTime.TryParseExact(processDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput))
                        {
                            Console.WriteLine("dateOutput" + DateTime.TryParseExact(processDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput) + dateOutput);

                            isDateValid = true;
                        }
                        else
                        {
                            Console.WriteLine("dateOutput" + DateTime.TryParseExact(processDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput) + dateOutput);
                            Console.WriteLine("The date you entered is either not a valid date or not in the format mmddyyyy, please try again or simple press the return/enter key to use the default GMT");

                            processDate = Console.ReadLine();
                        }
                    }

                }
                Console.WriteLine("Thank you for starting this program, the application is starting to retireve your csv file.");
            }
        }
    }
}
