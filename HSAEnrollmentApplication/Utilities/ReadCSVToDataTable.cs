using System;
using System.IO;
using System.Collections.Generic;
using System.Data;


namespace HSAEnrollmentApplication.Utilities
{
    public class ReadCSVToDataTable : IReadCSV
    {
        public DateTime TimeStamp = DateTime.UtcNow;
        public DataTable Table;
        public DateTime ProcessDate;

        ICSVType _csvType;
        public ReadCSVToDataTable(ICSVType csvType)
        {
            _csvType = csvType;
        }

        /// <summary>
        /// Processes any type of CSVType by row into a DataTable
        /// </summary
        public Response ProcessDataByRow(string csvPath, DateTime processDate, DataTable table)
        {
            try
            {
                using (StreamReader reader = new StreamReader(File.OpenRead(csvPath)))
                {

                    while (!reader.EndOfStream)
                    {
                        string row = reader.ReadLine();

                        List<string> fields = new List<string>(row.Split(","));

                        //validate initial
                        Response result = _csvType.ValidateInitialDataRow(fields);
                        if (!result.Success)
                        {
                            reader.Close();
                            Table.Clear();
                            return new Response(false, "A record in the file failed validation. Processing has stopped.");
                        }

                        //assessment
                        List<string> assessedRow = _csvType.ValidateCriteria(fields, processDate);

                        //read to table
                        _csvType.WriteToDataTable(table, fields);

                    }
                    reader.Close();
                }
                return new Response(true, "Validation complete, program successfully wrote data to a table in memory");
            }
            catch (System.IO.FileNotFoundException)
            {
                return new Response(false, "Your csv file path [" + csvPath + "] was not found in the system. The program is going to exit, please run the program again with a corrected file path.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to read and process csv data with " +
                   "CSVPath [" + csvPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }
        }

    }
}
