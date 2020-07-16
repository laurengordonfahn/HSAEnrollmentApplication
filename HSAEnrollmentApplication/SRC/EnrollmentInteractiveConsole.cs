using System;
using System.Globalization;
using System.IO;

namespace HSAEnrollmentApplication
{
    public class EnrollmentInteractiveConsole
    {
        public string WelcomeMsg = "Welcome to the Enrollment Application interactive console.";
        public bool shouldRequestProcessingDate = true;
        public string csvPath;
        public string processDate;

        public void EnrollmentStartInteractiveConsole()
        {
            Console.WriteLine(WelcomeMsg);
            Console.WriteLine("Please, enter the local path of the csv file you would like processed.");

            csvPath = Console.ReadLine();


            if (shouldRequestProcessingDate)
            {
                Console.WriteLine("At this time you may enter the date on which you would like this enrollment data to be processed against in mmddyyyy format " +
                    "OR" +
                    "you can press return and UTC aka GMT will be used.");

                processDate = Console.ReadLine();
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
                Console.WriteLine("Thank you for starting this program.");
            }
            
        }

        public void ReadCSV()
        {
            Console.WriteLine("The application is starting to retireve your csv file.");
            try
            {
                CSVReader csvReader = new CSVReader();
                csvReader.CSVPath = csvPath;

                Response response = csvReader.ReadCSVToMemoryStream();

                Console.WriteLine("response in console"  + response);
                Console.WriteLine("CSV file successfully read to memory data was valid.");
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
