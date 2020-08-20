using System;
using System.Data;
using HSAEnrollmentApplication.Models;

namespace HSAEnrollmentApplication.Utilities
{
    public class EnrollmentCSVProgram : IEnrollmentCSVProgram
    {
        public string WelcomeMsg = "Welcome to the Enrollment Application interactive console.";
        public string CSVPath;
        public bool ShouldRequestProcessingDate = true;
        public DateTime ProcessDate = System.DateTime.UtcNow.Date;
        public DataTable Table = new ProcessedDataTable().AssessmentTable();
        public string DateFormat;

        IConsoleCSVProcessor _consoleCSVProcessor;
        IReadCSV _readCSV;

        public EnrollmentCSVProgram(IConsoleCSVProcessor consoleCSVProcessor, IReadCSV readCSV)
        {
            _consoleCSVProcessor = consoleCSVProcessor;
            _readCSV = readCSV;
        }

        /// <summary>
        /// Runs the flow for the entire HSA Enrollment Console Program
        /// </summary>
        public void EnrollmentConsoleProgram(string format)
        {
            //From Console Arguements
            DateFormat = format;

            //Interactive Console
            _consoleCSVProcessor.WelcomeMessage(WelcomeMsg);
            CSVPath = _consoleCSVProcessor.GetCSVPath();
            ProcessDate = _consoleCSVProcessor.GetProcessDate(ShouldRequestProcessingDate, DateFormat);

            //Process Enrollment CSV Data
            Response response = _readCSV.ProcessDataByRow(CSVPath, ProcessDate, Table);

            if (response.Success)
            {
                Console.WriteLine(response.Message);
                DisplayData();
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
                for (int i = 0; i < Table.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.Write(Enum.Parse(typeof(AssessmentStatus), (dataRow.ItemArray[i]).ToString()) + " ");
                    }
                    else if (i == 4)
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

    }
}
