using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using HSAEnrollmentApplication.Models;

namespace HSAEnrollmentApplication
{
    public class EnrollmentInteractiveConsole
    {
        public string WelcomeMsg = "Welcome to the Enrollment Application interactive console.";
        public bool ShouldRequestProcessingDate = true;
        public string CSVPath;
        public string ProcessDate;
        public DataTable Table = new ProcessedDataTable().CreateTable();

        public void EnrollmentStartInteractiveConsole()
        {
            Console.WriteLine(WelcomeMsg);
            Console.WriteLine("Please, enter the local path of the csv file you would like processed.");

            CSVPath = Console.ReadLine();


            if (ShouldRequestProcessingDate)
            {
                Console.WriteLine("At this time you may enter the date on which you would like this enrollment data to be processed against in mmddyyyy format " +
                    "OR" +
                    "you can press return and UTC aka GMT will be used.");

                ProcessDate = Console.ReadLine();
                bool isDateValid = false;

                while (!isDateValid)
                {
                    if (String.IsNullOrEmpty(ProcessDate))
                    {
                        isDateValid = true;
                    }
                    else
                    {
                        DateTime dateOutput;
                        string format = "MMddyyyy";

                        if (DateTime.TryParseExact(ProcessDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput))
                        {
                            Console.WriteLine("dateOutput" + DateTime.TryParseExact(ProcessDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput) + dateOutput);

                            isDateValid = true;
                        }
                        else
                        {
                            Console.WriteLine("dateOutput" + DateTime.TryParseExact(ProcessDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput) + dateOutput);
                            Console.WriteLine("The date you entered is either not a valid date or not in the format mmddyyyy, please try again or simple press the return/enter key to use the default GMT");

                            ProcessDate = Console.ReadLine();
                        }
                    }

                }
                Console.WriteLine("Thank you for starting this program.");
            }

        }

        public DataTable ReadCSV()
        {
            Console.WriteLine("The application is starting to retireve your csv file.");

            Response response = CSVReader.ValidateCSVData(CSVPath, Table, ProcessDate);

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

        public DateTime CreateDataTable()
        {

        }

    }
}
