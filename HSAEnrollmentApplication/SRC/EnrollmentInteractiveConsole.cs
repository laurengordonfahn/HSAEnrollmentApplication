using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace HSAEnrollmentApplication
{
    public class EnrollmentInteractiveConsole
    {
        public string WelcomeMsg = "Welcome to the Enrollment Application interactive console.";
        public bool shouldRequestProcessingDate = true;
        public string csvPath;
        public string processDate;
        public CSVReader csvReader = new CSVReader();
        public MemoryStream memStream;

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

            csvReader.CSVPath = csvPath;

            Response response = csvReader.ValidateCSVData();

            if (response.Success)
            {
                Console.WriteLine(response.Message);
            }
            else
            {
                Console.WriteLine(response.Message);
                Environment.Exit(0);
            }
        }

        public void ReadCSVToMemory()
        {

            Console.WriteLine("Please wait while validated data is written to Memory for efficent processing of larger files.");
            Response response =  csvReader.ReadCSVToMemory();
            if (response.Success)
            {
                memStream = response.Stream;
                Console.WriteLine("CSV Data has successfully been read into memory to enable efficent processing of larger files.");
            }
            else
            {
                Console.WriteLine(response.Message);
                return;
            }
        }
    }
}
