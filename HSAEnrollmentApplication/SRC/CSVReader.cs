using System;
using System.IO;
using FluentValidation.Results;
using System.Text.Json;
using System.Collections.Generic;
using System.Data;
using HSAEnrollmentApplication.Models;
using System.Globalization;

namespace HSAEnrollmentApplication
{
    public class CSVReader
    {
        public string CSVPath { get; set; }
        public DataTable Table;
        //ApplicationSubmissionDate: date given to compare submitted data against 
        public DateTime ApplicationSubmissionDate { get; set; }
        public DateTime TimeStamp = DateTime.UtcNow;

        public CSVReader(string csvPath, DataTable table, DateTime processDate)
        {
            CSVPath = csvPath;
            Table = table;
            ApplicationSubmissionDate = processDate;
        }

        /// <summary>
        /// Validates each row of data
        /// </summary
        public Response ValidateCSVData()
        {

            try
            {
                using (StreamReader reader = new StreamReader(File.OpenRead(CSVPath)))
                {

                    while (!reader.EndOfStream)
                    {
                        string row = reader.ReadLine();

                        List<string> fields = new List<string>(row.Split(","));

                        //validate initial
                        Response result = ValidateInitialDataRow(fields);
                        if (!result.Success)
                        {
                            reader.Close();
                            Table.Clear();
                            return new Response(false, "A record in the file failed validation. Processing has stopped.");
                        }

                        //assessment
                        List<string> assessedRow = ValidateEnrollmentCriteria(fields);

                        //read to table
                        WriteToDataTable(fields);

                    }
                    reader.Close();
                }
                return new Response(true, "Validation complete, program successfully wrote data to a table in memory");
            }
            catch (System.IO.FileNotFoundException)
            {
                return new Response(false, "Your csv file path [" + CSVPath + "] was not found in the system. The program is going to exit, please run the program again with a corrected file path.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to read and process csv data with " +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }
        }

        /// <summary>
        /// Does intial data validation
        /// </summary
        public Response ValidateInitialDataRow(List<string> fields)
        {
            try
            {
                EnrollmentDataModel enrollmentRow = new EnrollmentDataModel(fields);
                //validate
                EnrollmentDataValidator validator = new EnrollmentDataValidator();
                ValidationResult results = validator.Validate(enrollmentRow);
                
                if (!results.IsValid)
                {
                    return new Response(false, "Data failed to validate" + JsonSerializer.Serialize(results.Errors));
                }
                return new Response(true, "Data validated");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to validate data row with " +
                   "Row [" + JsonSerializer.Serialize(fields) + "]" +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }

        }

        /// <summary>
        /// Accesses valid data for Enrollment status acceptablity
        /// </summary
        public List<string> ValidateEnrollmentCriteria(List<string> fields)
        {
            try
            {
                EnrollmentDataModel enrollmentRow = new EnrollmentDataModel(fields);
                //validate
                EnrollmentAssessmentValidator validator = new EnrollmentAssessmentValidator();
                validator.ApplicationSubmissionDate = ApplicationSubmissionDate;
                ValidationResult results = validator.Validate(enrollmentRow);
                if (!results.IsValid)
                {
                    fields.Add("Rejected");
                }
                else
                {
                    fields.Add("Accepted");
                }
                return fields;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to assess data row with " +
                   "Row [" + JsonSerializer.Serialize(fields) + "]" +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }

        }

        /// <summary>
        /// Writes a row of data to the data table in memory
        /// </summary
        public void WriteToDataTable(List<string> fields)
        {
            try
            {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                DateTime dob = DateTime.ParseExact(fields[2], "MMddyyyy", CultureInfo.InvariantCulture);
                DateTime effectiveDate = DateTime.ParseExact(fields[4], "MMddyyyy", CultureInfo.InvariantCulture);

                string shortDOB = dob.ToShortDateString();
                string shortEffectiveDate = effectiveDate.ToShortDateString();
                AssessmentStatus status = (AssessmentStatus)Enum.Parse(typeof(AssessmentStatus), fields[5]);
                PlanType plan = (PlanType)Enum.Parse(typeof(PlanType), fields[3]);

                Table.Rows.Add(status, fields[0], fields[1], shortDOB, plan, shortEffectiveDate);
                Table.AcceptChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown trying to write data row to table with " +
                   "Row [" + JsonSerializer.Serialize(fields) + "]" +
                   "CSVPath [" + CSVPath + "]" +
                   "Exception [" + e + "]" +
                   "at [" + TimeStamp + "]");
                throw;
            }
        }
    }
}
