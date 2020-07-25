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
        public DateTime ProcessDate = System.DateTime.UtcNow.Date;
        public DataTable Table = new ProcessedDataTable().AssessmentTable();

        /// <summary>
        /// Prompts user for csv path and a date to process csv data against. If not process date is submitted utc is used as default.
        /// </summary
        public void EnrollmentStartInteractiveConsole()
        {
            Console.WriteLine(WelcomeMsg);
       
            GetCSVPath();

            GetProcessDate();
           
            Console.WriteLine("Thank you for starting this program.");
        }

        /// <summary>
        /// Handles CSV processing and informs user of progress: Initiates CSV stream reader and validates data and access status before writing to datatable
        /// </summary>
        public void ReadCSV()
        {
            Console.WriteLine("The application is starting to retrieve your csv file.");

            Response response = new CSVReader(CSVPath, Table, ProcessDate).ValidateCSVData();
    

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

        /// <summary>
        /// Writes processed data rows to console
        /// </summary>
        public void DisplayData()
        {
            Console.WriteLine("Processed data displayed below:");
            foreach (DataRow dataRow in Table.Rows)
            {
                for(int i = 0; i < Table.Columns.Count;  i++)
                {
                    if(i == 0)
                    {
                        Console.Write(Enum.Parse(typeof(AssessmentStatus), (dataRow.ItemArray[i]).ToString()) + " ");
                    }
                    else if(i == 4)
                    {
                        Console.Write(Enum.Parse(typeof(PlanType), (dataRow.ItemArray[i]).ToString()) + " ");
                    }
                    else
                    {
                        Console.Write(dataRow.ItemArray[i] + " ");
                    }    
                }
                Console.WriteLine();
            }
            Console.WriteLine("Program complete.");
            return;
        }

        /// <summary>
        /// Console Prompts for getting the csv path
        /// </summary>
        public void GetCSVPath ()
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
                Console.WriteLine("At this time you may enter the date on which you would like this enrollment data to be processed against in mmddyyyy format " +
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
                        string format = "MMddyyyy";

                        if (DateTime.TryParseExact(submittedDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput))
                        {
                            ProcessDate = dateOutput;
                            return;
                        }
                        else
                        {
                            Console.WriteLine("The date you entered is either not a valid date or not in the format mmddyyyy, please try again or simply press the return/enter key to use the default GMT");
                            submittedDate = Console.ReadLine();
                        }
                    }

                }
            }
            return;
        }
    
    }
}
