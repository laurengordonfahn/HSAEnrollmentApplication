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
        public DateTime ProcessDate;
        public DataTable Table = new ProcessedDataTable().AssessmentTable();
        

        public void EnrollmentStartInteractiveConsole()
        {
            Console.WriteLine(WelcomeMsg);
            Console.WriteLine("Please, enter the local path of the csv file you would like processed.");

            CSVPath = Console.ReadLine();
            Console.WriteLine(CSVPath);

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
                        ProcessDate = System.DateTime.UtcNow.Date;
                        isDateValid = true;
                    }
                    else
                    {
                        DateTime dateOutput;
                        string format = "MMddyyyy";

                        if (DateTime.TryParseExact(submittedDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput))
                        {
                            Console.WriteLine("dateOutput" + DateTime.TryParseExact(submittedDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput) + dateOutput);
                            ProcessDate = dateOutput;
                            isDateValid = true;
                        }
                        else
                        {
                            Console.WriteLine("dateOutput" + DateTime.TryParseExact(submittedDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateOutput) + dateOutput);
                            Console.WriteLine("The date you entered is either not a valid date or not in the format mmddyyyy, please try again or simple press the return/enter key to use the default GMT");

                            submittedDate = Console.ReadLine();
                        }
                    }

                }
                Console.WriteLine("Thank you for starting this program.");
            }

        }

        public void ReadCSV()
        {
            Console.WriteLine("The application is starting to retireve your csv file.");

            Response response = new CSVReader(CSVPath, Table, ProcessDate).ValidateCSVData();
    

            if (response.Success)
            {
                Console.WriteLine("Successfully validated data and assessed enrollment status!");
            }
            else
            {
                Console.WriteLine("A record in the file failed validation.  Processing has stopped.");
                Environment.Exit(0);
            }
        }

        public void DisplayData()
        {
            foreach (DataRow dataRow in Table.Rows)
            {
                Console.WriteLine(Table.Columns.Count);

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

        }

    }
}
